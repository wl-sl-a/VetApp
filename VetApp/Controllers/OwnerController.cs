using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Core.Services;
using VetApp.Core.Models;
using AutoMapper;
using VetApp.Resources;
using Microsoft.AspNetCore.Authorization;
using VetApp.Authentication;
using VetApp.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace VetApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerService ownerService;
        private readonly IPasswordService passwordService;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        public OwnerController(IOwnerService ownerService, IMapper mapper, IAuthRepository authRepository, IPasswordService passwordService, IEmailService emailService)
        {
            this.mapper = mapper;
            this.ownerService = ownerService;
            this.authRepository = authRepository;
            this.passwordService = passwordService;
            this.emailService = emailService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<OwnerResource>>> GetAll()
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var owners = await ownerService.GetAll(iden);
                var ownerResource = mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerResource>>(owners);
                return Ok(ownerResource);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var ownerResource = mapper.Map<Owner, OwnerResource>(owner);
                return Ok(ownerResource);
            }
        }

        [HttpGet("search/{param}")]
        public async Task<ActionResult<IEnumerable<OwnerResource>>> Search(string param)
        {
            string iden = User.Identity.Name;
            var owners = await ownerService.Search(iden, param);
            var ownerResource = mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerResource>>(owners);
            return Ok(ownerResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResource>> GetOwnerById(int id)
        {
            string iden = User.Identity.Name;
            var owner = await ownerService.GetOwnerById(id, iden);
            var ownerResource = mapper.Map<Owner, OwnerResource>(owner);
            return Ok(ownerResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<OwnerResource>> CreateOwner([FromBody] OwnerResource ownerResource)
        {
            string iden = User.Identity.Name;
            RegisterOwnerModel registerOwnerModel = new RegisterOwnerModel();
            registerOwnerModel.Username = ownerResource.Username;
            registerOwnerModel.Email = ownerResource.Email;
            registerOwnerModel.VetName = iden;
            registerOwnerModel.Password = passwordService.GeneratePassword(10, 20);
            emailService.Send(registerOwnerModel.Username, registerOwnerModel.Password);
            await RegisterOwner(registerOwnerModel);
            var ownerToCreate = mapper.Map<OwnerResource, Owner>(ownerResource);
            ownerToCreate.VetName = iden;
            var newOwner = await ownerService.CreateOwner(ownerToCreate);
            var owner = await ownerService.GetOwnerById(newOwner.Id, iden);
            var ownerResResource = mapper.Map<Owner, OwnerResource>(owner);
            return Ok(ownerResResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OwnerResource>> DeleteOwner(int id)
        {
            string iden = User.Identity.Name;
            var owner = await ownerService.GetOwnerById(id, iden);
            if (owner != null) await ownerService.DeleteOwner(owner);
            return Ok(owner);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OwnerResource>> UpdateOwner(int id, [FromBody] OwnerResource ownerResource)
        {
            string iden = User.Identity.Name;
            ownerResource.VetName = iden;
            var owner = mapper.Map<OwnerResource, Owner>(ownerResource);
            await ownerService.UpdateOwner(id, owner);

            var updatedOwner = await ownerService.GetOwnerById(id, iden);
            var updatedOwnerResource = mapper.Map<Owner, OwnerResource>(updatedOwner);
            return Ok(updatedOwnerResource);
        }

        public async Task<IActionResult> RegisterOwner(RegisterOwnerModel model)
        {
            var userExists = await authRepository.FindByName(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await authRepository.Create(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
