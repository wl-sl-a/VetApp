using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAll(string iden);
        Task<Schedule> GetScheduleById(int id, string iden);
        Task<IEnumerable<Schedule>> GetSchedulesByDoctorId(int doctorId, string iden);
        Task<Schedule> CreateSchedule(Schedule newSchedule, string iden);
        Task UpdateSchedule(int id, Schedule schedule);
        Task DeleteSchedule(Schedule schedule);
        IEnumerable<string> GetDates(int doctorId, string iden);
        IEnumerable<string> GetTimes(string date, int doctorId, string iden);
        IEnumerable<string> GetFreeTimes(string date, int doctorId, string iden);
    }
}
