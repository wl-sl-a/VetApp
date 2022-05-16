using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace Aquarium.DAL.Repository
{
    public class FishRepository : Repository<Fish>, IFishRepository
    {
        public FishRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Fish>> GetAllAsync(string iden)
        {
            return await Context.Set<Fish>().Where(p => p.NameCompany == iden).ToListAsync();
        }

        public ValueTask<Fish> GetFishByIdAsync(int id, string iden)
        {
            var a = Context.Set<Fish>().Where(p => p.NameCompany == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var fish = Context.Set<Fish>().FindAsync(id);
                return fish;
            }
            return new ValueTask<Fish>();
        }

        public async Task<IEnumerable<Fish>> GetAllByAquaIdAsync(int aquaId, string iden)
        {
            return await ApplicationDbContext.Fishes
                .Include(m => m.Aquarium)
                .Where(m => m.AquariumId == aquaId)
                .Where(m => m.NameCompany == iden)
                .ToListAsync();
        }

        public bool CheckAqua(int id, string iden)
        {
            var a = Context.Set<AquariumM>().Where(p => p.NameCompany == iden).Where(p => p.Id == id).ToList();
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
