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
    public class DirectionController : Controller
    {
        private readonly IDirectionService directionService;
        private readonly IMapper mapper;

        public DirectionController(IDirectionService directionService, IMapper mapper)
        {
            this.mapper = mapper;
            this.directionService = directionService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DirectionResource>>> GetAllDirections()
        {
            string iden = User.Identity.Name;
            var directions = await directionService.GetAll(iden);
            var directionResource = mapper.Map<IEnumerable<Direction>, IEnumerable<DirectionResource>>(directions);

            return Ok(directionResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectionResource>> GetDirectionById(int id)
        {
            string iden = User.Identity.Name;
            var direction = await directionService.GetDirectionById(id, iden);
            var directionResource = mapper.Map<Direction, DirectionResource>(direction);
            return Ok(directionResource);
        }

        [HttpGet("animal/{id}")]
        public async Task<ActionResult<IEnumerable<DirectionResource>>> GetDirectionsByAnimalId(int id)
        {
            string iden = User.Identity.Name;
            var direction = await directionService.GetDirectionsByAnimalId(id, iden);
            var directionResource = mapper.Map<IEnumerable<Direction>, IEnumerable<DirectionResource>>(direction);
            return Ok(directionResource);
        }

        [HttpGet("visiting/{id}")]
        public async Task<ActionResult<IEnumerable<DirectionResource>>> GetDirectionsByVisitingId(int id)
        {
            string iden = User.Identity.Name;
            var direction = await directionService.GetDirectionsByVisitingId(id, iden);
            var directionResource = mapper.Map<IEnumerable<Direction>, IEnumerable<DirectionResource>>(direction);
            return Ok(directionResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<DirectionResource>> CreateDirection([FromBody] DirectionResource directionResource)
        {
            var directionToCreate = mapper.Map<DirectionResource, Direction>(directionResource);
            string iden = User.Identity.Name;
            var newDirection = await directionService.CreateDirection(directionToCreate, iden);
            var direction = await directionService.GetDirectionById(newDirection.Id, iden);
            var directionResourc = mapper.Map<Direction, DirectionResource>(direction);

            return Ok(directionResourc);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirection(int id)
        {
            string iden = User.Identity.Name;
            var direction = await directionService.GetDirectionById(id, iden);
            if (direction != null) await directionService.DeleteDirection(direction);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DirectionResource>> UpdateDirection(int id, [FromBody] DirectionResource directionResource)
        {
            string iden = User.Identity.Name;
            var direction = mapper.Map<DirectionResource, Direction>(directionResource);
            await directionService.UpdateDirection(id, direction);

            var updatedDirection = await directionService.GetDirectionById(id, iden);
            var updatedDirectionResource = mapper.Map<Direction, DirectionResource>(updatedDirection);
            return Ok(updatedDirectionResource);
        }
    }
}
