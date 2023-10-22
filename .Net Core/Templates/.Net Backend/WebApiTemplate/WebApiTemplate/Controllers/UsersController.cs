using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiTemplate.DTOs.UsersDTOs;
using WebApiTemplate.Services.Exceptions;
using WebApiTemplate.Services.UserService;

namespace WebApiTemplate.Controllers
{
    [ApiController]
    [Route("api/users")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController: ControllerBase
    {
        private readonly IUserService usersService;

        public UsersController(IUserService usersService)
        {
            this.usersService = usersService;
        }        

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<UserFullDTO>>> GetAllUsers()
        {
            return await usersService.GetAllUsers();
        }

        [HttpGet("getAllRoles")]
        public async Task<ActionResult<List<RoleDTO>>> GetAllRoles()
        {
            return await usersService.GetAllRoles();
        }

        [HttpPost("createRole")]
        public async Task<ActionResult> CreateRole(string model)
        {
            try
            {
                var result = await usersService.CreateRole(model);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("An error has occurred while trying to create the role!");
                }
            }
            catch (Exception)
            {
                return BadRequest("An error has occurred while trying to create the role!");
            }
        }        

        [HttpPost("assignRole")]
        public async Task<ActionResult> AssignUserRole(EditRoleDTO model)
        {
            try
            {
                var result = await usersService.AssignUserRole(model);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception)
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost("unassignRole")]
        public async Task<ActionResult> UnAssignUserRole(EditRoleDTO model)
        {
            try
            {
                var result = await usersService.UnAssignUserRole(model);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception)
            {
                return BadRequest("User not found");
            }
        }

        [HttpDelete("deleteRole")]
        public async Task<ActionResult> DeleteRole(string model)
        {
            try
            {
                var result = await usersService.DeleteRole(model);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("An error has occurred while trying to delete the role!");
                }
            }
            catch (Exception)
            {
                return BadRequest("An error has occurred while trying to delete the role!");
            }
        }

        [HttpPut("updateUser/{id}")]
        public async Task<ActionResult> UpdateUser(string id, [FromBody] UserEditDTO user)
        {
            try
            {
                var result = await usersService.EditUser(user, id);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("An error has occurred while trying to update the user!");
                }
            }
            catch (MissingDataException e) 
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("An error has occurred while trying to update the user!");
            }
        }
        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var result = await usersService.DeleteUser(id);

                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("An error has occurred while trying to update the user!");
                }
            }
            catch (MissingDataException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("An error has occurred while trying to update the user!");
            }
        }
    }
}
