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
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;

        public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
        {
            this.mapper = mapper;
            this.appointmentService = appointmentService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AppointmentResource>>> GetAllAppointments()
        {
            string iden = User.Identity.Name;
            var appointments = await appointmentService.GetAll(iden);
            var appointmentResource = mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointments);

            return Ok(appointmentResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalResource>> GetAppointmentById(int id)
        {
            string iden = User.Identity.Name;
            var appointment = await appointmentService.GetAppointmentById(id, iden);
            var appointmentResource = mapper.Map<Appointment, AppointmentResource>(appointment);
            return Ok(appointmentResource);
        }

        [HttpGet("animal/{id}")]
        public async Task<ActionResult<IEnumerable<AppointmentResource>>> GetAppointmentsByAnimalId(int id)
        {
            string iden = User.Identity.Name;
            var appointment = await appointmentService.GetAppointmentsByAnimalId(id, iden);
            var appointmentResource = mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointment);
            return Ok(appointmentResource);
        }

        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<IEnumerable<AppointmentResource>>> GetAppointmentsByDoctorId(int id)
        {
            string iden = User.Identity.Name;
            var appointment = await appointmentService.GetAppointmentsByDoctorId(id, iden);
            var appointmentResource = mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointment);
            return Ok(appointmentResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<AppointmentResource>> CreateAppointment([FromBody] AppointmentResource appointmentResource)
        {
            if (User.IsInRole(UserRoles.Company))
            {
                var appointmentToCreate = mapper.Map<AppointmentResource, Appointment>(appointmentResource);
                string iden = User.Identity.Name;
                var newAppointment = await appointmentService.CreateAppointment(appointmentToCreate, iden);
                var appointment = await appointmentService.GetAppointmentById(newAppointment.Id, iden);
                var appointmentResourc = mapper.Map<Appointment, AppointmentResource>(appointment);

                return Ok(appointmentResourc);
            }
            else
            {
                var appointmentToCreate = mapper.Map<AppointmentResource, Appointment>(appointmentResource);
                string iden = User.Identity.Name;
                var newAppointment = await appointmentService.CreateAppointment(appointmentToCreate, iden);
                var appointment = await appointmentService.GetAppointmentById(newAppointment.Id, iden);
                var appointmentResourc = mapper.Map<Appointment, AppointmentResource>(appointment);

                return Ok(appointmentResourc);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            string iden = User.Identity.Name;
            var appointment = await appointmentService.GetAppointmentById(id, iden);
            if (appointment != null) await appointmentService.DeleteAppointment(appointment);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalResource>> UpdateAnimal(int id, [FromBody] AppointmentResource appointmentResource)
        {
            string iden = User.Identity.Name;
            var appointment = mapper.Map<AppointmentResource, Appointment>(appointmentResource);
            await appointmentService.UpdateAppointment(id, appointment);

            var updatedAppointment = await appointmentService.GetAppointmentById(id, iden);
            var updatedAppointmentResource = mapper.Map<Appointment, AppointmentResource>(updatedAppointment);
            return Ok(updatedAppointmentResource);
        }

        [HttpPost("app_dates/{doctorId}")]
        public async Task<ActionResult<IEnumerable<AppointmentTime>>> GetTimesByDate(int doctorId, [FromBody] string date)
        {
            string iden = User.Identity.Name;
            var times = await appointmentService.GetAppointmentsByDoctorIdDate(doctorId, iden, date);
            var free = 0;
            var taken = 0;
            foreach(var time in times)
            {
                if (time.Status == "free") free++;
                if (time.Status == "taken") taken++;
            }
           
            return Ok(times);
        }

        [HttpPost("workload/{doctorId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetWorkloadByDate(int doctorId, [FromBody] string date)
        {
            string iden = User.Identity.Name;
            var times = await appointmentService.GetAppointmentsByDoctorIdDate(doctorId, iden, date);
            var free = 0;
            var taken = 0;
            foreach (var time in times)
            {
                if (time.Status == "free") free++;
                if (time.Status == "taken") taken++;
            }

            return Ok(new { free, taken});
        }
    }
}