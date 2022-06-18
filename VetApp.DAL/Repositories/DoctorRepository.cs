using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace VetApp.DAL.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string iden)
        {
            return await Context.Set<Doctor>().Where(p => p.VetName == iden).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> Search(string iden, string param)
        {
            return await Context.Set<Doctor>().Where(p => p.VetName == iden).Where(p => p.Surname.Contains(param) || 
            p.Name.Contains(param) || p.Specialty.Contains(param)).ToListAsync();
        }

        public ValueTask<Doctor> GetByIdAsync(int id, string iden)
        {
            var a = Context.Set<Doctor>().Where(p => p.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var doctor = Context.Set<Doctor>().FindAsync(id);
                return doctor;
            }
            return new ValueTask<Doctor>();
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
