using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.DTOs.AccountDTOs;
using WebApiTemplate.Services.AccountService;

namespace WebApiTemplate.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController: ControllerBase
    {        
        private readonly IAccountService accountsService;

        public AccountsController(IAccountService accountsService)
        {           
            this.accountsService = accountsService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDTO>> CreateUser([FromBody] UserInfoDTO model){

            var result = await accountsService.CreateUser(model);

            if (result.Succeeded)
            {
                LoginDTO loginDTO = new LoginDTO
                {
                    UserName = model.UserName,
                    Password = model.Password,
                };
                return await accountsService.BuildToken(loginDTO);
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        [HttpPost("registerAdmin")]
        public async Task<ActionResult<UserTokenDTO>> CreateUserAdmin([FromBody] UserInfoDTO model)
        {

            var result = await accountsService.CreateUserAdmin(model);

            if (result.Succeeded)
            {
                LoginDTO loginDTO = new LoginDTO
                {
                    UserName = model.UserName,
                    Password = model.Password,
                };
                return await accountsService.BuildToken(loginDTO);
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody]LoginDTO model)
        {
            var result = await accountsService.Login(model);

            if (result.Succeeded)
            {
                return await accountsService.BuildToken(model);
            }
            else
            {
                return BadRequest("Login Faild");
            }
        }

        [HttpGet("renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTokenDTO>> Renew()
        {
            var userInfo = new LoginDTO()
            {
                UserName = HttpContext.User.Identity!.Name!
            };

            return await accountsService.BuildToken(userInfo);
        }
    }
}
