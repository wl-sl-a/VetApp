using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IDirectionRepository : IRepository<Direction>
    {
        Task<IEnumerable<Direction>> GetAllAsync(string iden);
        ValueTask<Direction> GetDirectionByIdAsync(int id, string iden);
        Task<IEnumerable<Direction>> GetAllByVisitingIdAsync(int visitingId, string iden);
        Task<IEnumerable<Direction>> GetAllByAnimalIdAsync(int animalId, string iden);
        bool CheckVet(int id, string iden);
    }
}
