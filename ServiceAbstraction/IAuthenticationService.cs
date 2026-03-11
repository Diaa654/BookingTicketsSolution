using Shared.CommonResult;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto);
        Task<Result<string>> RegisterAsync(RegisterDto registerDto);
        Task<Result<string>> ForgetPassword(ForgotPasswordRequestDto email);
        Task<Result<string>> VerifyOtp(OtpDto otpDto);
        Task<Result<string>> ResetPasswordAsync(RestPasswordDto restPassword);
        Task<Result<string>> ConfirmEmailAsync(ConfirmEmailDto confirmEmail);
    }
}
