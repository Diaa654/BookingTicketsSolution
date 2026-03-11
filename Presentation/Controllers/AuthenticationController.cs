using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServicesAbstraction;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManger serviceManger) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var result = await serviceManger.authenticationService.LoginAsync(loginDto);
            return HandleResult<UserDto>(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterAsync(RegisterDto registerDto)
        {
            var result = await serviceManger.authenticationService.RegisterAsync(registerDto);
            return HandleResult<string>(result);
        }
        [HttpPost("ForgetPassword")]
        [EnableRateLimiting("IdentityPolicy")]
        public async Task<ActionResult<string>> ForgetPassword(ForgotPasswordRequestDto email)
        {
            var res=await serviceManger.authenticationService.ForgetPassword(email);
            return HandleResult(res);
        }
        [HttpPost("VerifyOtp")]
        [EnableRateLimiting("IdentityPolicy")]
        public async Task<ActionResult<string>> VerifyOtp(OtpDto OtpDto)
        {
            var result = await serviceManger.authenticationService.VerifyOtp(OtpDto);
            return HandleResult(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(RestPasswordDto resetPasswordRequestDto)
        {
            var result= await serviceManger.authenticationService.ResetPasswordAsync(resetPasswordRequestDto);
            return HandleResult(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery]ConfirmEmailDto emailDto)
        {
            var result = await serviceManger.authenticationService.ConfirmEmailAsync(emailDto);
            return HandleResult(result);
        }
        
    }
}
