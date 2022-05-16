using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Task<IEnumerable<Animal>> GetAllAsync(string iden);
        ValueTask<Animal> GetAnimalByIdAsync(int id, string iden);
        Task<IEnumerable<Animal>> GetAllByOwnerIdAsync(int ownerId, string iden);
        bool CheckAqua(int id, string iden);
    }
}
