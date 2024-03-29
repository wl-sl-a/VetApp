﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAsync(string iden);
        ValueTask<Appointment> GetAppointmentByIdAsync(int id, string iden);
        Task<IEnumerable<Appointment>> GetAllByAnimalIdAsync(int animalId, string iden);
        Task<IEnumerable<Appointment>> GetAllByDoctorIdAsync(int doctorId, string iden);
        bool CheckVet(int id, string iden);
        bool CheckAppointmentByDoctorIdDateTimeAsync(int doctorId, string date, string time, string iden);
        ValueTask<Appointment> GetAppointmentByDateTimeAsync(int doctorId, string date, string time, string iden);
        Task<IEnumerable<Appointment>> FilterAllAsync(string iden, string status, int? aid, int? did, int? sid);
    }
}
