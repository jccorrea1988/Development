using Microsoft.AspNetCore.Identity;
using WebApiTemplate.Entities;

namespace WebApiTemplate.Repositories
{
    public class UsersRepository
    {
        private readonly ApplicationDbContext context;

        public UsersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ApplicationUser> GetAllUsers() {
            return context.Users.AsQueryable();            
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            return context.Roles.AsQueryable();
        }
    }
}
