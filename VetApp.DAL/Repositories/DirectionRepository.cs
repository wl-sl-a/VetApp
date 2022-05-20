using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;

namespace VetApp.DAL.Repositories
{
    public class DirectionRepository : Repository<Direction>, IDirectionRepository
    {
        public DirectionRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Direction>> GetAllAsync(string iden)
        {
            return await Context.Set<Direction>().Where(p => p.Visiting.Animal.Owner.VetName == iden || p.Visiting.Animal.Owner.Username == iden).ToListAsync();
        }

        public ValueTask<Direction> GetDirectionByIdAsync(int id, string iden)
        {
            var a = Context.Set<Direction>().Where(p => p.Visiting.Animal.Owner.VetName == iden || p.Visiting.Animal.Owner.Username == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var direction = Context.Set<Direction>().FindAsync(id);
                return direction;
            }
            return new ValueTask<Direction>();
        }

        public async Task<IEnumerable<Direction>> GetAllByVisitingIdAsync(int visitingId, string iden)
        {
            return await ApplicationDbContext.Directions
                .Include(m => m.Visiting)
                .Where(m => m.VisitingId == visitingId)
                .Where(m => m.Visiting.Animal.Owner.VetName == iden)
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
