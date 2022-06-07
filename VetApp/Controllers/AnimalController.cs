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
    public class AnimalController : Controller
    {
        private readonly IAnimalService animalService;
        private readonly IOwnerService ownerService;
        private readonly IMapper mapper;

        public AnimalController(IAnimalService animalService, IMapper mapper, IOwnerService ownerService)
        {
            this.mapper = mapper;
            this.animalService = animalService;
            this.ownerService = ownerService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AnimalResource>>> GetAllAnimals()
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var animals = await animalService.GetAll(iden);
                var animalResource = mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalResource>>(animals);

                return Ok(animalResource);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var animals = await animalService.GetAnimalsByOwnerId(owner.Id, owner.VetName);
                var animalResource = mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalResource>>(animals);

                return Ok(animalResource);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalResource>> GetAnimalById(int id)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var animal = await animalService.GetAnimalById(id, iden);
                var animalResource = mapper.Map<Animal, AnimalResource>(animal);
                return Ok(animalResource);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var animal = await animalService.GetAnimalById(id, owner.VetName);
                var animalResource = mapper.Map<Animal, AnimalResource>(animal);
                return Ok(animalResource);
            }
        }

        [HttpGet("owner/{id}")]
        public async Task<ActionResult<IEnumerable<AnimalResource>>> GetAnimalsByOwnerId(int id)
        {
            string iden = User.Identity.Name;
            var animal = await animalService.GetAnimalsByOwnerId(id, iden);
            var animalResource = mapper.Map<IEnumerable<Animal>, IEnumerable<AnimalResource>>(animal);
            return Ok(animalResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<AnimalResource>> CreateAnimal([FromBody] AnimalResource animalResource)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                var animalToCreate = mapper.Map<AnimalResource, Animal>(animalResource);
                string iden = User.Identity.Name;
                var newAnimal = await animalService.CreateAnimal(animalToCreate, iden);
                var animal = await animalService.GetAnimalById(newAnimal.Id, iden);
                var animalResourc = mapper.Map<Animal, AnimalResource>(animal);

                return Ok(animalResourc);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var animalToCreate = mapper.Map<AnimalResource, Animal>(animalResource);
                animalToCreate.OwnerId = owner.Id;
                var newAnimal = await animalService.CreateAnimal(animalToCreate, owner.VetName);
                var animal = await animalService.GetAnimalById(newAnimal.Id, owner.VetName);
                var animalResourc = mapper.Map<Animal, AnimalResource>(animal);

                return Ok(animalResourc);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var animal = await animalService.GetAnimalById(id, iden);
                if (animal != null) await animalService.DeleteAnimal(animal);
                return NoContent();
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var animal = await animalService.GetAnimalById(id, owner.VetName);
                if (animal != null) await animalService.DeleteAnimal(animal);
                return NoContent();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalResource>> UpdateAnimal(int id, [FromBody] AnimalResource animalResource)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var animal = mapper.Map<AnimalResource, Animal>(animalResource);
                await animalService.UpdateAnimal(id, animal);

                var updatedAnimal = await animalService.GetAnimalById(id, iden);
                var updatedAnimalResource = mapper.Map<Animal, AnimalResource>(updatedAnimal);
                return Ok(updatedAnimalResource);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var animal = mapper.Map<AnimalResource, Animal>(animalResource);
                animal.OwnerId = owner.Id;
                await animalService.UpdateAnimal(id, animal);

                var updatedAnimal = await animalService.GetAnimalById(id, owner.VetName);
                var updatedAnimalResource = mapper.Map<Animal, AnimalResource>(updatedAnimal);
                return Ok(updatedAnimalResource);
            }
            
        }
    }
}
