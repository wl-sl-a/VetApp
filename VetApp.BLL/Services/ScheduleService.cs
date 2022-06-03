using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;
using System;

namespace VetApp.BLL.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork unitOfWork;
        public ScheduleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Schedule> CreateSchedule(Schedule newSchedule, string iden)
        {
            if (unitOfWork.Schedules.CheckDoctor(newSchedule.DoctorId, iden))
            {
                await unitOfWork.Schedules.AddAsync(newSchedule);
                await unitOfWork.CommitAsync();

                return newSchedule;
            }
            else return new Schedule();
        }

        public async Task DeleteSchedule(Schedule schedule)
        {
            unitOfWork.Schedules.Remove(schedule);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAll(string iden)
        {
            return await unitOfWork.Schedules
                .GetAllAsync(iden);
        }

        public async Task<Schedule> GetScheduleById(int id, string iden)
        {
            return await unitOfWork.Schedules
                .GetScheduleByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByDoctorId(int doctorId, string iden)
        {
            return await unitOfWork.Schedules
                .GetAllByDoctorIdAsync(doctorId, iden);
        }

        public async Task UpdateSchedule(int id, Schedule schedule)
        {
            schedule.Id = id;
            unitOfWork.Schedules.Entry(schedule);
            await unitOfWork.CommitAsync();
        }

        public IEnumerable<string> GetDates(int doctorId, string iden)
        {
            List<string> result = new List<string>();
            DateTime date = DateTime.Now;
            string weekday = date.DayOfWeek.ToString();
            var schedules = GetSchedulesByDoctorId(doctorId, iden);
            for (int i = 0; i < 30; i++)
            {
                foreach(Schedule schedule in schedules.Result)
                {
                    if (schedule.Weekday == weekday) result.Add(date.ToString("d"));
                }
                date = date.AddDays(1);
                weekday = date.DayOfWeek.ToString();
            }
            return result;
        }

        public IEnumerable<string> GetTimes(string date, int doctorId, string iden)
        {
            List<string> result = new List<string>();
            DateTime dateTime = DateTime.Parse(date);
            string weekday = dateTime.DayOfWeek.ToString();
            var schedules = GetSchedulesByDoctorId(doctorId, iden);
            string start = "";
            string end = "";
            foreach (Schedule schedule in schedules.Result)
            {
                if(schedule.Weekday == weekday)
                {
                    start = schedule.StartTime;
                    end = schedule.EndTime;
                }
            }
            DateTime startTime = DateTime.ParseExact(start, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(end, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            while(startTime < endTime)
            {
                result.Add(startTime.ToString("t"));
                startTime = startTime.AddMinutes(20);
            }
            return result;
        }

        public IEnumerable<string> GetFreeTimes(string date, int doctorId, string iden)
        {
            List<string> result = new List<string>();
            var allTimes = GetTimes(date, doctorId, iden);
            foreach(string time in allTimes)
            {
                if(!unitOfWork.Appointments.CheckAppointmentByDoctorIdDateTimeAsync(doctorId, date, time, iden))
                {
                    result.Add(time);
                }
            }
            return result;
        }
    }
}
