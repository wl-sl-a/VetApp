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

namespace VetApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;
        private readonly IOwnerService ownerService;
        public DoctorController(IDoctorService doctorService, IMapper mapper, IOwnerService ownerService)
        {
            this.mapper = mapper;
            this.doctorService = doctorService;
            this.ownerService = ownerService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DoctorResource>>> GetAll()
        {
            if (User.IsInRole(UserRoles.Company))
            {
                string iden = User.Identity.Name;
                var doctors = await doctorService.GetAll(iden);
                var doctorResource = mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorResource>>(doctors);
                return Ok(doctorResource);
            }
            else
            {
                string username = User.Identity.Name;
                var owner = ownerService.GetOwnerByUsername(username);
                var doctors = await doctorService.GetAll(owner.VetName);
                var doctorResource = mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorResource>>(doctors);
                return Ok(doctorResource);
            }
        }

        [HttpGet("search/{param}")]
        public async Task<ActionResult<IEnumerable<DoctorResource>>> Search(string param)
        {
            string iden = User.Identity.Name;
            var doctors = await doctorService.Search(iden, param);
            var doctorResource = mapper.Map<IEnumerable<Doctor>, IEnumerable<OwnerResource>>(doctors);
            return Ok(doctorResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResource>> GetDoctorById(int id)
        {
            string iden = User.Identity.Name;
            var doctor = await doctorService.GetDoctorById(id, iden);
            var doctorResource = mapper.Map<Doctor, DoctorResource>(doctor);
            return Ok(doctorResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<DoctorResource>> CreateDoctor([FromBody] DoctorResource doctorResource)
        {
            var doctorToCreate = mapper.Map<DoctorResource, Doctor>(doctorResource);
            string iden = User.Identity.Name;
            doctorToCreate.VetName = iden;
            var newDoctor = await doctorService.CreateDoctor(doctorToCreate);
            var doctor = await doctorService.GetDoctorById(newDoctor.Id, iden);
            var doctorResResource = mapper.Map<Doctor, DoctorResource>(doctor);
            return Ok(doctorResResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorResource>> DeleteDoctor(int id)
        {
            string iden = User.Identity.Name;
            var doctor = await doctorService.GetDoctorById(id, iden);
            if (doctor != null) await doctorService.DeleteDoctor(doctor);
            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorResource>> UpdateDoctor(int id, [FromBody] DoctorResource doctorResource)
        {
            string iden = User.Identity.Name;
            doctorResource.VetName = iden;
            var doctor = mapper.Map<DoctorResource, Doctor>(doctorResource);
            await doctorService.UpdateDoctor(id, doctor);

            var updatedDoctor = await doctorService.GetDoctorById(id, iden);
            var updatedDoctorResource = mapper.Map<Doctor, DoctorResource>(updatedDoctor);
            return Ok(updatedDoctorResource);
        }
    }
}
