using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetAllAsync(string iden);
        ValueTask<Schedule> GetScheduleByIdAsync(int id, string iden);
        Task<IEnumerable<Schedule>> GetAllByDoctorIdAsync(int doctorId, string iden);
        bool CheckDoctor(int id, string iden);
    }
}
