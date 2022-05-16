using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AuthRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public Task<ApplicationUser> FindByName(string name)
        {
            return this.userManager.FindByNameAsync(name);
        }

        public Task<bool> CheckPassword(ApplicationUser user, string password)
        {
            return this.userManager.CheckPasswordAsync(user, password);
        }

        public Task<IList<string>> GetRoles(ApplicationUser user)
        {
            return userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            return userManager.CreateAsync(user, password);
        }

        public Task<bool> RoleExists(string role)
        {
            return roleManager.RoleExistsAsync(role);
        }

        public Task<IdentityResult> CreateRole(IdentityRole role)
        {
            return roleManager.CreateAsync(role);
        }

        public Task<IdentityResult> AddToRole(ApplicationUser user, string role)
        {
            return userManager.AddToRoleAsync(user, role);
        }
    }
}
