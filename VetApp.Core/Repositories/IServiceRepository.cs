using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetAllAsync(string iden);
        ValueTask<Service> GetByIdAsync(int id, string iden);
        Task<IEnumerable<Service>> Search(string iden, string param);
    }
}