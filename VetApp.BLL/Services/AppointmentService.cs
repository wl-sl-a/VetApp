using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;
using System;

namespace VetApp.BLL.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IScheduleService scheduleService;
        public AppointmentService(IUnitOfWork unitOfWork, IScheduleService scheduleService)
        {
            this.unitOfWork = unitOfWork;
            this.scheduleService = scheduleService;
        }

        public async Task<Appointment> CreateAppointment(Appointment newAppointment, string iden)
        {
            newAppointment.Animal = await unitOfWork.Animals.GetAnimalByIdAsync(newAppointment.AnimalId, iden);
            if(newAppointment.Animal != null && !unitOfWork.Appointments.CheckAppointmentByDoctorIdDateTimeAsync(newAppointment.DoctorId,
                newAppointment.Date, newAppointment.Time, iden))
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

        public async Task<IEnumerable<Appointment>> FilterAll(string iden, string status, int? aid, int? did, int? sid)
        {
            return await unitOfWork.Appointments
                .FilterAllAsync(iden, status, aid, did, sid);
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

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId, string iden)
        {
            return await unitOfWork.Appointments
                .GetAllByDoctorIdAsync(doctorId, iden);
        }

        public async Task UpdateAppointment(int id, Appointment appointment)
        {
            appointment.Id = id;
            unitOfWork.Appointments.Entry(appointment);
            await unitOfWork.CommitAsync();
        }

        public async Task ConfirmAppointment(int id, string iden)
        {
            var appointment = GetAppointmentById(id, iden).Result;
            appointment.Id = id;
            appointment.Status = "confirmed";
            unitOfWork.Appointments.Entry(appointment);
            await unitOfWork.CommitAsync();
        }

        public async Task CancelAppointment(int id, string iden)
        {
            var appointment = GetAppointmentById(id, iden).Result;
            appointment.Id = id;
            appointment.Status = "cancelled";
            unitOfWork.Appointments.Entry(appointment);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<AppointmentTime>> GetAppointmentsByDoctorIdDate(int doctorId, string iden, string date)
        {
            List<AppointmentTime> appointmentTimes = new List<AppointmentTime>();
            var times = scheduleService.GetTimes(date, doctorId, iden);
            int i = 1;
            foreach(String time in times)
            {
                if(unitOfWork.Appointments.CheckAppointmentByDoctorIdDateTimeAsync(doctorId, date, time, iden))
                {
                    var at = new AppointmentTime();
                    at.Id = i;
                    at.Time = time;
                    at.Status = "taken";
                    at.Appointment = unitOfWork.Appointments.GetAppointmentByDateTimeAsync(doctorId, date, time, iden).Result.Id;
                    appointmentTimes.Add(at);
                }
                else
                {
                    var at = new AppointmentTime();
                    at.Id = i;
                    at.Time = time;
                    at.Status = "free";
                    appointmentTimes.Add(at);
                }
                i++;
            }
            return appointmentTimes;
        }

        private bool CheckDateTime(int doctorId, string date, string time, string iden)
        {
            var schedules = unitOfWork.Schedules.GetAllByDoctorIdAsync(doctorId, iden);
            DateTime date1 = DateTime.ParseExact(date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime time1 = DateTime.ParseExact(time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            foreach(Schedule schedule in schedules.Result)
            {

                if(schedule.Weekday == date1.DayOfWeek.ToString() 
                    && DateTime.ParseExact(schedule.StartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture) >= time1
                    && DateTime.ParseExact(schedule.EndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture) < time1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
