using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace VetApp.DAL.Repositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Owner>> GetAllAsync(string iden)
        {
            return await Context.Set<Owner>().Where(p => p.VetName == iden).ToListAsync();
        }

        public async Task<IEnumerable<Owner>> Search(string iden, string param)
        {
            return await Context.Set<Owner>().Where(p => p.VetName == iden).Where(p => p.Surname.Contains(param) || p.Name.Contains(param)
            || p.Phone.Contains(param)).ToListAsync();
        }

        public ValueTask<Owner> GetByIdAsync(int id, string iden)
        {
            var a = Context.Set<Owner>().Where(p => p.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var owner = Context.Set<Owner>().FindAsync(id);
                return owner;
            }
            return new ValueTask<Owner>();
        }

        public Owner GetByOwnerByUsernameAsync(string username)
        {
            var a = Context.Set<Owner>().Where(p => p.Username == username).ToList();
            if (a.Count == 1)
            {
                var owner = a.ElementAt(0);
                return owner;
            }
            return new Owner();
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
