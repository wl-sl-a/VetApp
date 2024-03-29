﻿using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Repositories;
using VetApp.DAL.Repositories;

namespace VetApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private AnimalRepository animalRepository;
        private OwnerRepository ownerRepository;
        private DoctorRepository doctorRepository;
        private AppointmentRepository appointmentRepository;
        private VisitingRepository visitingRepository;
        private DirectionRepository directionRepository;
        private ServiceRepository serviceRepository;
        private ScheduleRepository scheduleRepository;
        private AdminRepository adminRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IAnimalRepository Animals => animalRepository = animalRepository ?? new AnimalRepository(context);

        public IOwnerRepository Owners => ownerRepository = ownerRepository ?? new OwnerRepository(context);

        public IDoctorRepository Doctors => doctorRepository = doctorRepository ?? new DoctorRepository(context);

        public IAppointmentRepository Appointments => appointmentRepository = appointmentRepository ?? new AppointmentRepository(context);

        public IVisitingRepository Visitings => visitingRepository = visitingRepository ?? new VisitingRepository(context);

        public IDirectionRepository Directions => directionRepository = directionRepository ?? new DirectionRepository(context);

        public IServiceRepository Services => serviceRepository = serviceRepository ?? new ServiceRepository(context);
        public IScheduleRepository Schedules => scheduleRepository = scheduleRepository ?? new ScheduleRepository(context);
        public IAdminRepository Admin => adminRepository = adminRepository ?? new AdminRepository();

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
