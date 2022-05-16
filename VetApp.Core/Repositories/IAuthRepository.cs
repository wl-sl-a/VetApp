using System;
using System.Collections.Generic;
using System.Text;
using VetApp.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VetApp.Core.Repositories
{
    public interface IAuthRepository
    {
        public Task<ApplicationUser> FindByName(string name);
        public Task<bool> CheckPassword(ApplicationUser user, string password);
        public Task<IList<string>> GetRoles(ApplicationUser user);
        public Task<IdentityResult> Create(ApplicationUser user, string password);
        public Task<bool> RoleExists(string role);
        public Task<IdentityResult> CreateRole(IdentityRole role);
        public Task<IdentityResult> AddToRole(ApplicationUser user, string role);
    }
}
