using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Appointment>> GetAllAsync(string iden)
        {
            return await Context.Set<Appointment>().Where(p => p.Animal.Owner.VetName == iden || p.Animal.Owner.Username == iden).ToListAsync();
        }

        public ValueTask<Appointment> GetAppointmentByIdAsync(int id, string iden)
        {
            var a = Context.Set<Appointment>().Where(p => p.Animal.Owner.VetName == iden || p.Animal.Owner.Username == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var appointment = Context.Set<Appointment>().FindAsync(id);
                return appointment;
            }
            return new ValueTask<Appointment>();
        }

        public async Task<IEnumerable<Appointment>> GetAllByAnimalIdAsync(int animalId, string iden)
        {
            return await ApplicationDbContext.Appointments
                .Include(m => m.Animal)
                .Where(m => m.AnimalId == animalId)
                .Where(m => m.Animal.Owner.VetName == iden)
                .ToListAsync();
        }

        public bool CheckVet(int id, string iden)
        {
            var a = Context.Set<Owner>().Where(p => p.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                return true;
            }
            return false;
        }

        public bool CheckAppointmentByDoctorIdDateTimeAsync(int doctorId, string date, string time, string iden)
        {
            var a = Context.Set<Appointment>().Where(p => p.Doctor.VetName == iden).Where(p => p.DoctorId == doctorId)
                .Where(p => p.Date == date).Where(p => p.Time == time).ToList();
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
