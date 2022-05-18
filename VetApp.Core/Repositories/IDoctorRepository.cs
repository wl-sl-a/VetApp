using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllAsync(string iden);
        ValueTask<Doctor> GetByIdAsync(int id, string iden);
        Task<IEnumerable<Doctor>> Search(string iden, string param);
    }
}
