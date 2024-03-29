﻿using System;
using System.Threading.Tasks;
using VetApp.Core.Repositories;

namespace VetApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IOwnerRepository Owners { get; }
        IAnimalRepository Animals { get; }
        IDoctorRepository Doctors { get; }
        IVisitingRepository Visitings { get; }
        IAppointmentRepository Appointments { get; }
        IDirectionRepository Directions { get; }
        IServiceRepository Services { get; }
        IScheduleRepository Schedules { get; }
        IAdminRepository Admin { get; }
        Task<int> CommitAsync();
    }
}

