using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.DTOs.UsersDTOs;

namespace WebApiTemplate.Services.UserService
{
    public interface IUserService
    {
        public Task<IdentityResult> CreateRole(string name);
        public Task<IdentityResult> DeleteRole(string name);
        public Task<IdentityResult> AssignUserRole(EditRoleDTO model);
        public Task<IdentityResult> UnAssignUserRole(EditRoleDTO model);
        public Task<ActionResult<List<UserFullDTO>>> GetAllUsers();
        public Task<ActionResult<List<RoleDTO>>> GetAllRoles();
        public Task<IdentityResult> EditUser(UserEditDTO user, string id);
        public Task<IdentityResult> DeleteUser(string id);
    }
}
