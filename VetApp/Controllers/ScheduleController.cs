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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly IMapper mapper;
       
        public ScheduleController(IScheduleService scheduleService, IMapper mapper)
        {
            this.mapper = mapper;
            this.scheduleService = scheduleService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ScheduleResource>>> GetAll()
        {
            string iden = User.Identity.Name;
            var schedules = await scheduleService.GetAll(iden);
            var scheduleResource = mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(schedules);
            return Ok(scheduleResource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleResource>> GetScheduleById(int id)
        {
            string iden = User.Identity.Name;
            var schedule = await scheduleService.GetScheduleById(id, iden);
            var scheduleResource = mapper.Map<Schedule, ScheduleResource>(schedule);
            return Ok(scheduleResource);
        }

        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<IEnumerable<ScheduleResource>>> GetSchedulesByAnimalId(int id)
        {
            string iden = User.Identity.Name;
            var schedule = await scheduleService.GetSchedulesByDoctorId(id, iden);
            var scheduleResource = mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(schedule);
            return Ok(scheduleResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ScheduleResource>> CreateSchedule([FromBody] ScheduleResource scheduleResource)
        {
            var scheduleToCreate = mapper.Map<ScheduleResource, Schedule>(scheduleResource);
            string iden = User.Identity.Name;
            var newSchedule = await scheduleService.CreateSchedule(scheduleToCreate, iden);
            var schedule = await scheduleService.GetScheduleById(newSchedule.Id, iden);
            var scheduleResResource = mapper.Map<Schedule, ScheduleResource>(schedule);
            return Ok(scheduleResResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ScheduleResource>> DeleteOwner(int id)
        {
            string iden = User.Identity.Name;
            var schedule = await scheduleService.GetScheduleById(id, iden);
            if (schedule != null) await scheduleService.DeleteSchedule(schedule);
            return Ok(schedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ScheduleResource>> UpdateSchedule(int id, [FromBody] ScheduleResource scheduleResource)
        {
            string iden = User.Identity.Name;
            var schedule = mapper.Map<ScheduleResource, Schedule>(scheduleResource);
            await scheduleService.UpdateSchedule(id, schedule);

            var updatedSchedule = await scheduleService.GetScheduleById(id, iden);
            var updatedScheduleResource = mapper.Map<Schedule, ScheduleResource>(updatedSchedule);
            return Ok(updatedScheduleResource);
        }

        [HttpGet("doctor_dates/{doctorId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetDates(int doctorId)
        {
            string iden = User.Identity.Name;
            var dates = scheduleService.GetDates(doctorId, iden);
            return Ok(dates);
        }

        [HttpGet("doctor_times/{doctorId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetTimes(int doctorId, [FromBody] string date)
        {
            string iden = User.Identity.Name;
            var times = scheduleService.GetFreeTimes(date, doctorId, iden);
            return Ok(times);
        }
    }
}
