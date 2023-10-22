using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.DTOs.UsersDTOs;
using WebApiTemplate.Entities;
using WebApiTemplate.Repositories;
using WebApiTemplate.Services.Exceptions;
using WebApiTemplate.Services.UserService;

namespace WebApiTemplate.Services
{
    public class UsersService: IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UsersRepository usersRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersService(UserManager<ApplicationUser> userManager, UsersRepository usersRepository, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRole(string name)
        {
            bool exist = await roleManager.RoleExistsAsync(name);
            if (!exist)
            {
                var role = new IdentityRole();
                role.Name = name;
                return await roleManager.CreateAsync(role);
            }
            throw new Exception("Role already exists!");
        }

        public async Task<IdentityResult> DeleteRole(string name)
        {
            bool exist = await roleManager.RoleExistsAsync(name);
            if (exist)
            {
                var role = await roleManager.FindByNameAsync(name);
                return await roleManager.DeleteAsync(role!);
            }
            throw new Exception("Role does not exists!");
        }

        public async Task<IdentityResult> AssignUserRole(EditRoleDTO model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            return await userManager.AddToRoleAsync(user, model.Role);            
        }

        public async Task<IdentityResult> UnAssignUserRole(EditRoleDTO model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            return await userManager.RemoveFromRoleAsync(user, model.Role);
        }

        public async Task<ActionResult<List<UserFullDTO>>> GetAllUsers()
        {
            var queryable = usersRepository.GetAllUsers();
            return await queryable.Select(x => new UserFullDTO { Id = x.Id, Name = x.UserName!, Email = x.Email, PhoneNumber = x.PhoneNumber }).ToListAsync();
        }

        public async Task<ActionResult<List<RoleDTO>>> GetAllRoles()
        {
            var queryable = usersRepository.GetAllRoles();
            return await queryable.Select(x => new RoleDTO { Name = x.Name!, Id = x.Id! }).ToListAsync();
        }

        public async Task<IdentityResult> EditUser(UserEditDTO user, string id)
        {
            var usr = await userManager.FindByIdAsync(id);
            AssertUserExists(id);
            usr.PhoneNumber = user.PhoneNumber != null ? user.PhoneNumber : usr.PhoneNumber;
            usr.Email = user.Email != null ? user.Email : usr.Email;
            usr.UserName = user.Username != null ? user.Username : usr.UserName;
            return await userManager.UpdateAsync(usr);   
        }

        public async Task<IdentityResult> DeleteUser(string id) 
        {
            
            var usr = await userManager.FindByIdAsync(id);
            AssertUserExists(id);
            return await userManager.DeleteAsync(usr);
        }

        private async void AssertUserExists(string id) 
        {
            var usr = await userManager.FindByIdAsync(id);
            if (usr == null) 
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
