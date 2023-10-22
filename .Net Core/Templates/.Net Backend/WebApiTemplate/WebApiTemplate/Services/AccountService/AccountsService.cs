using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTemplate.DTOs.AccountDTOs;
using WebApiTemplate.Entities;
using WebApiTemplate.Repository;
using WebApiTemplate.Services.AccountService;

namespace WebApiTemplate.Services
{
    public class AccountsService:IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountsService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {            
            this.userManager = userManager;            
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> CreateUser(UserInfoDTO model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
            var result = await userManager.CreateAsync(user, model.Password);

            var userRol = await userManager.FindByIdAsync(user.Id);
            if (userRol is null)
            {
                throw new Exception("User not found");
            }

            return await userManager.AddToRoleAsync(userRol, "admin");
        }

        public async Task<IdentityResult> CreateUserAdmin(UserInfoDTO model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
            var result = await userManager.CreateAsync(user, model.Password);
            var userRol = await userManager.FindByIdAsync(user.Id);

            if (userRol is null)
            {
                throw new Exception("User not found");
            }

            return await userManager.AddToRoleAsync(userRol, "admin");
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName,
               model.Password,
               isPersistent: false,
               lockoutOnFailure: false);

            return result;
        }

        public async Task<UserTokenDTO> BuildToken(LoginDTO userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.UserName),
            };

            var user = await userManager.FindByNameAsync(userInfo.UserName);
            var roles = await userManager.GetRolesAsync(user!);
           
            claims.Add(new Claim("Email", user.Email ?? ""));
            claims.Add(new Claim("Id", user.Id ?? ""));
            claims.Add(new Claim("Phone Number", user.PhoneNumber ?? ""));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(2);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );
            return new UserTokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
