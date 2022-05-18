using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork unitOfWork;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Doctor> CreateDoctor(Doctor newDoctor)
        {
            await unitOfWork.Doctors
                .AddAsync(newDoctor);
            await unitOfWork.CommitAsync();

            return newDoctor;
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
            unitOfWork.Doctors.Remove(doctor);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAll(string iden)
        {
            return await unitOfWork.Doctors.GetAllAsync(iden);
        }

        public async Task<IEnumerable<Doctor>> Search(string iden, string param)
        {
            return await unitOfWork.Doctors.Search(iden, param);
        }

        public async Task<Doctor> GetDoctorById(int id, string iden)
        {
            return await unitOfWork.Doctors.GetByIdAsync(id, iden);
        }

        public async Task UpdateDoctor(int id, Doctor doctor)
        {
            doctor.Id = id;
            unitOfWork.Doctors.Entry(doctor);
            await unitOfWork.CommitAsync();
        }
    }
}
