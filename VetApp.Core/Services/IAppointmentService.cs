using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAll(string iden);
        Task<Appointment> GetAppointmentById(int id, string iden);
        Task<IEnumerable<Appointment>> GetAppointmentsByAnimalId(int animalId, string iden);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId, string iden);
        Task<IEnumerable<AppointmentTime>> GetAppointmentsByDoctorIdDate(int doctorId, string iden, string date);
        Task<Appointment> CreateAppointment(Appointment newAppointment, string iden);
        Task UpdateAppointment(int id, Appointment appointment);
        Task DeleteAppointment(Appointment appointment);
    }
}
