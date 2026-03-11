using Domain.Contracts;
using Domain.Modules.UserModule;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared;

using System.Text;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Shared.CommonResult;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager
        , IUnitOfWork _unitOfWork, IConfiguration _configuration
        , ICacheRepository cache) : IAuthenticationService
    {

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null)
            {
                return Error.NotFound("User Is Not Found", $"The User With  Email:{loginDto.Email} is Not Found");
            }
            else if (!await _userManager.IsEmailConfirmedAsync(User))
            {
                return Error.NotFound("Need To Confirm", $"User With Email: {loginDto.Email} Should confirm his email before logging in.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (isPasswordValid)
            {
                return new UserDto
                {
                    FirstName = User.FirstName,
                    Email = User.Email!,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                return Error.InvalidCredentials("Invalid Credentials", "The Email Or Password is Incorrect");
            }
        }
        public async Task<Result<string>> RegisterAsync(RegisterDto registerDto)
        {
            var User = await _userManager.FindByEmailAsync(registerDto.Email);

            if (User != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(User))
                {
                    return Error.Unauthorized("Email Not Confirmed", $"The Email {registerDto.Email} is already registered but not confirmed. Please check your email to confirm your account.");
                }
                else return Error.Conflict("Email Already Registered", $"The Email {registerDto.Email} is already registered. Please use a different email.");
            }
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Passenger");


                var passenger = new Passenger
                {
                    User = user,
                    DateOfBirth = registerDto.DateOfBirth
                };
                await _unitOfWork.GetRepository<Passenger, int>().AddAsync(passenger);
                await _unitOfWork.SaveChangesAsync();


                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

                var confirmationLink = $"https://localhost:7296/api/Authentication/ConfirmEmail?email={user.Email}&token={encodedToken}";

                var emailMessage = new Email()
                {
                    To = user.Email,
                    Subject = "Confirm Your Email",
                    Body = $"Welcome {user.FirstName},To Active Your Account Follow this Link: <a href='{confirmationLink}'>اضغط هنا</a>"
                };
                var isSent = await EmailSettings.SendEmail(emailMessage);

                if (!isSent)
                {
                    return Error.Failure("Email.ServiceUnavailable", "Failed to send the verification email. Please try again later.");
                }

                return Result<string>.Ok("Your Account Has Registered Successfully,Please Check Your Email To Active Your it");

            }
            else
            {
                var errors = result.Errors
                  .Select(e => Error.Validation(e.Code, e.Description))
                  .ToList();
                

                return errors;
            }
        }



        public async Task<Result<string>> ForgetPassword(ForgotPasswordRequestDto emailDto)
        {
           
            var user = await _userManager.FindByEmailAsync(emailDto.Email);
            if (user == null)
            {
                return Error.NotFound("User.NotFound", $"The email {emailDto.Email} is not registered.");
            }

            var otp = new Random().Next(100000, 999999).ToString();
            await cache.SetAsync(emailDto.Email, otp, TimeSpan.FromMinutes(10));
            var emailMessage = new Email()
            {
                To = emailDto.Email,
                Subject = "Reset Password OTP",
                Body = $"Your verification code is: {otp}. It will expire in 10 minutes."
            };

            var isSent = await EmailSettings.SendEmail(emailMessage);

            
            if (!isSent)
            {
                return Error.Failure("Email.ServiceUnavailable", "Failed to send the verification email. Please try again later.");
            }

            return Result<string>.Ok("Verification code sent to your email.");
        }
        public async Task<Result<string>> VerifyOtp(OtpDto otpDto)
        {
            var user = await _userManager.FindByEmailAsync(otpDto.Email);
            if (user == null) return Error.NotFound("User.NotFound", $"The email {otpDto.Email} is not registered.");   
            var savedOtp = await cache.GetAsync(otpDto.Email);

            if (string.IsNullOrEmpty(savedOtp) || savedOtp != otpDto.Code)
            {
               return Error.Validation("Invalid OTP", "The OTP is expired or not valid");
            }
            return Result<string>.Ok("Correct Otp");


        }

        public async Task<Result<string>> ResetPasswordAsync(RestPasswordDto restPassword)
        {
            var user = await _userManager.FindByEmailAsync(restPassword.Email);
            if (user == null) return Error.NotFound("User.NotFound", $"The email {restPassword.Email} is not registered.");

            var savedOtp = await cache.GetAsync(restPassword.Email);

            if (string.IsNullOrEmpty(savedOtp) || savedOtp != restPassword.Code)
            {
                return Error.Validation("Invalid OTP", "The OTP is expired or not valid");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var result = await _userManager.ResetPasswordAsync(user, token, restPassword.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors
              .Select(e => Error.Validation(e.Code, e.Description))
              .ToList();

                return errors;
            }

            await cache.RemoveAsync(restPassword.Email);

            return Result<string>.Ok("Your Password Has rest Successfully");
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var Claims = new List<Claim>
           {

            new (ClaimTypes.Email,user.Email!),
            new (ClaimTypes.Name,user.UserName!),
            new (ClaimTypes.NameIdentifier,user.Id.ToString()),
           };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

       

        public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailDto emailDto)
        {
            var user = await _userManager.FindByEmailAsync(emailDto.Email);
            if (user == null) throw new Exception("User Not Found");


            var decodedTokenBytes = WebEncoders.Base64UrlDecode(emailDto.Token);
            var originalToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, originalToken);

            if (!result.Succeeded)
            {
                var errors = result.Errors
               .Select(e => Error.Validation(e.Code, e.Description))
               .ToList();

                return errors;
            }
            return Result<string>.Ok("Your Account Has been active Successfully");
        }
    }
}
