using Microsoft.AspNetCore.Identity;
using WebApiTemplate.DTOs.AccountDTOs;

namespace WebApiTemplate.Services.AccountService
{
    public interface IAccountService
    {
        public Task<IdentityResult> CreateUser(UserInfoDTO model);
        public Task<IdentityResult> CreateUserAdmin(UserInfoDTO model);
        public Task<SignInResult> Login(LoginDTO model);
        public Task<UserTokenDTO> BuildToken(LoginDTO userInfo);
    }
}
