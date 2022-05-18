using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Animal>> GetAllAsync(string iden)
        {
            return await Context.Set<Animal>().Where(p => p.Owner.VetName == iden || p.Owner.Username == iden).ToListAsync();
        }

        public ValueTask<Animal> GetAnimalByIdAsync(int id, string iden)
        {
            var a = Context.Set<Animal>().Where(p => p.Owner.VetName == iden || p.Owner.Username == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var animal = Context.Set<Animal>().FindAsync(id);
                return animal;
            }
            return new ValueTask<Animal>();
        }

        public async Task<IEnumerable<Animal>> GetAllByOwnerIdAsync(int ownerId, string iden)
        {
            return await ApplicationDbContext.Animals
                .Include(m => m.Owner)
                .Where(m => m.OwnerId == ownerId)
                .Where(m => m.Owner.VetName == iden)
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
