using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<IEnumerable<Owner>> GetAllAsync(string iden);
        ValueTask<Owner> GetByIdAsync(int id, string iden);
        Task<IEnumerable<Owner>> Search(string iden, string param);
    }
}
