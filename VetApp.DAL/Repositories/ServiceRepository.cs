using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApp.Core.Models;
using VetApp.Core.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace VetApp.DAL.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Service>> GetAllAsync(string iden)
        {
            return await Context.Set<Service>().Where(p => p.VetName == iden).ToListAsync();
        }

        public async Task<IEnumerable<Service>> Search(string iden, string param)
        {
            return await Context.Set<Service>().Where(p => p.VetName == iden).Where(p => p.Name.Contains(param)).ToListAsync();
        }

        public ValueTask<Service> GetByIdAsync(int id, string iden)
        {
            var a = Context.Set<Service>().Where(p => p.VetName == iden).Where(p => p.Id == id).ToList();
            if (a.Count == 1)
            {
                var service = Context.Set<Service>().FindAsync(id);
                return service;
            }
            return new ValueTask<Service>();
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
