using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll(string iden);
        Task<Doctor> GetDoctorById(int id, string iden);
        Task<Doctor> CreateDoctor(Doctor newDoctor);
        Task UpdateDoctor(int id, Doctor doctor);
        Task DeleteDoctor(Doctor doctor);
        Task<IEnumerable<Doctor>> Search(string iden, string param);
    }
}
