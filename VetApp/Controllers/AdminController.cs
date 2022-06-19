using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VetApp.Authentication;
using Microsoft.AspNetCore.Authorization;
using VetApp.Core.Services;

namespace VetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("getRole")]
        public bool GetRole()
        {
            return User.IsInRole(UserRoles.Admin);
        }

        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("backup")]
        public string GetBackUp()
        {
            return adminService.GetBackup();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("restore/{filepath}")]
        public string GetRestore(string filepath)
        {
            return adminService.GetRestore(filepath);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("ssl")]
        public DateTime GetSsl()
        {
            return adminService.GetExpirationSsl();
        }
    }
}
