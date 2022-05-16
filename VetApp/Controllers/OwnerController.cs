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

namespace VetApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerService ownerService;
        private readonly IMapper mapper;
        public OwnerController(IOwnerService ownerService, IMapper mapper)
        {
            this.mapper = mapper;
            this.ownerService = ownerService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<OwnerResource>>> GetAll()
        {
            string iden = User.Identity.Name;
            var owners = await ownerService.GetAll(iden);
            var ownerResource = mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerResource>>(owners);
            return Ok(ownerResource);
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
            var ownerToCreate = mapper.Map<OwnerResource, Owner>(ownerResource);
            string iden = User.Identity.Name;
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
    }
}
