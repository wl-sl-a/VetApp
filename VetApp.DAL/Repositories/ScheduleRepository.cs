using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Schedule>> GetAllAsync(string iden)
        {
            return await Context.Set<Schedule>().Where(p => p.Doctor.VetName == iden).ToListAsync();
        }

        public ValueTask<Schedule> GetScheduleByIdAsync(int id, string iden)
        {
            var a = Context.Set<Schedule>().Where(p => p.Doctor.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var schedule = Context.Set<Schedule>().FindAsync(id);
                return schedule;
            }
            return new ValueTask<Schedule>();
        }

        public async Task<IEnumerable<Schedule>> GetAllByDoctorIdAsync(int doctorId, string iden)
        {
            return await ApplicationDbContext.Schedules
                .Include(m => m.Doctor)
                .Where(m => m.DoctorId == doctorId)
                .Where(m => m.Doctor.VetName == iden)
                .ToListAsync();
        }

        public bool CheckDoctor(int id, string iden)
        {
            var a = Context.Set<Doctor>().Where(p => p.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                return true;
            }
            return false;
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
