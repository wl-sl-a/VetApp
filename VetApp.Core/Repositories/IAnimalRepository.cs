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
        Task<IEnumerable<Animal>> Search(string iden, string param);
        Task<IEnumerable<Animal>> SearchByOwnerId(string iden, string param, int ownerId);
        bool CheckVet(int id, string iden);
    }
}
