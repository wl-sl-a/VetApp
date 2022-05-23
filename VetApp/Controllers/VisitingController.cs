using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Services;
using VetApp.Core.Models;
using AutoMapper;
using VetApp.Resources;
using VetApp.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace VetApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitingController : Controller
    {
        private readonly IVisitingService visitingService;
        private readonly IMapper mapper;

        public VisitingController(IVisitingService visitingService, IMapper mapper)
        {
            this.mapper = mapper;
            this.visitingService = visitingService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<VisitingResource>>> GetAllVisitings()
        {
            string iden = User.Identity.Name;
            var visitings = await visitingService.GetAll(iden);
            var visitingResource = mapper.Map<IEnumerable<Visiting>, IEnumerable<VisitingResource>>(visitings);

            return Ok(visitingResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitingResource>> GetVisitingById(int id)
        {
            string iden = User.Identity.Name;
            var visiting = await visitingService.GetVisitingById(id, iden);
            var visitingResource = mapper.Map<Visiting, VisitingResource>(visiting);
            return Ok(visitingResource);
        }

        [HttpGet("animal/{id}")]
        public async Task<ActionResult<IEnumerable<VisitingResource>>> GetVisitingsByAnimalId(int id)
        {
            string iden = User.Identity.Name;
            var visiting = await visitingService.GetVisitingsByAnimalId(id, iden);
            var visitingResource = mapper.Map<IEnumerable<Visiting>, IEnumerable<VisitingResource>>(visiting);
            return Ok(visitingResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<VisitingResource>> CreateVisiting([FromBody] VisitingResource visitingResource)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                var visitingToCreate = mapper.Map<VisitingResource, Visiting>(visitingResource);
                string iden = User.Identity.Name;
                var newVisiting = await visitingService.CreateVisiting(visitingToCreate, iden);
                var visiting = await visitingService.GetVisitingById(newVisiting.Id, iden);
                var visitingResourc = mapper.Map<Visiting, VisitingResource>(visiting);

                return Ok(visitingResourc);
            }
            else
            {
                var visitingToCreate = mapper.Map<VisitingResource, Visiting>(visitingResource);
                string iden = User.Identity.Name;
                var newVisiting = await visitingService.CreateVisiting(visitingToCreate, iden);
                var visiting = await visitingService.GetVisitingById(newVisiting.Id, iden);
                var visitingResourc = mapper.Map<Visiting, VisitingResource>(visiting);

                return Ok(visitingResourc);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisiting(int id)
        {
            string iden = User.Identity.Name;
            var visiting = await visitingService.GetVisitingById(id, iden);
            if (visiting != null) await visitingService.DeleteVisiting(visiting);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VisitingResource>> UpdateVisiting(int id, [FromBody] VisitingResource visitingResource)
        {
            string iden = User.Identity.Name;
            var visiting = mapper.Map<VisitingResource, Visiting>(visitingResource);
            await visitingService.UpdateVisiting(id, visiting);

            var updatedVisiting = await visitingService.GetVisitingById(id, iden);
            var updatedVisitingResource = mapper.Map<Visiting, VisitingResource>(updatedVisiting);
            return Ok(updatedVisitingResource);
        }
    }
}
