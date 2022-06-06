using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IVisitingRepository : IRepository<Visiting>
    {
        Task<IEnumerable<Visiting>> GetAllAsync(string iden);
        ValueTask<Visiting> GetVisitingByIdAsync(int id, string iden);
        Task<IEnumerable<Visiting>> GetAllByAnimalIdAsync(int animalId, string iden);
        Task<IEnumerable<Visiting>> GetAllByDoctorIdAsync(int animalId, string iden);
        bool CheckVet(int id, string iden);
    }
}