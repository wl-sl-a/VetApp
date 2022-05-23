using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Appointment> CreateAppointment(Appointment newAppointment, string iden)
        {
            newAppointment.Animal = await unitOfWork.Animals.GetAnimalByIdAsync(newAppointment.AnimalId, iden);
            if(newAppointment.Animal != null)
            {
                if (unitOfWork.Appointments.CheckVet(newAppointment.Animal.OwnerId, iden))
                {
                    await unitOfWork.Appointments.AddAsync(newAppointment);
                    await unitOfWork.CommitAsync();
                    return newAppointment;
                }
                else
                {
                    var owner = unitOfWork.Owners.GetByOwnerByUsernameAsync(iden);
                    newAppointment.Animal.Owner = owner;
                    newAppointment.Animal.OwnerId = owner.Id;
                    await unitOfWork.Appointments.AddAsync(newAppointment);
                    await unitOfWork.CommitAsync();
                    return newAppointment;
                }
            }
            return new Appointment();
        }

        public async Task DeleteAppointment(Appointment appointment)
        {
            unitOfWork.Appointments.Remove(appointment);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAll(string iden)
        {
            return await unitOfWork.Appointments
                .GetAllAsync(iden);
        }

        public async Task<Appointment> GetAppointmentById(int id, string iden)
        {
            return await unitOfWork.Appointments
                .GetAppointmentByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByAnimalId(int animalId, string iden)
        {
            return await unitOfWork.Appointments
                .GetAllByAnimalIdAsync(animalId, iden);
        }

        public async Task UpdateAppointment(int id, Appointment appointment)
        {
            appointment.Id = id;
            unitOfWork.Appointments.Entry(appointment);
            await unitOfWork.CommitAsync();
        }
    }
}
