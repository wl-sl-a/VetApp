using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class VisitingRepository : Repository<Visiting>, IVisitingRepository
    {
        public VisitingRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Visiting>> GetAllAsync(string iden)
        {
            return await Context.Set<Visiting>().Where(p => p.Animal.Owner.VetName == iden || p.Animal.Owner.Username == iden).ToListAsync();
        }

        public ValueTask<Visiting> GetVisitingByIdAsync(int id, string iden)
        {
            var a = Context.Set<Visiting>().Where(p => p.Animal.Owner.VetName == iden || p.Animal.Owner.Username == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var visiting = Context.Set<Visiting>().FindAsync(id);
                return visiting;
            }
            return new ValueTask<Visiting>();
        }

        public async Task<IEnumerable<Visiting>> GetAllByAnimalIdAsync(int animalId, string iden)
        {
            return await ApplicationDbContext.Visitings
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

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
