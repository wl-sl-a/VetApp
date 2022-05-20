using AutoMapper;
using VetApp.Resources;
using VetApp.Core.Models;

namespace VetApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Owner, OwnerResource>();
            CreateMap<Animal, AnimalResource>();
            CreateMap<Doctor, DoctorResource>();
            CreateMap<Appointment, AppointmentResource>();
            CreateMap<Visiting, VisitingResource>();
            CreateMap<Direction, DirectionResource>();

            // Resource to Domain
            CreateMap<OwnerResource, Owner>();
            CreateMap<AnimalResource, Animal>();
            CreateMap<DoctorResource, Doctor>();
            CreateMap<AppointmentResource, Appointment>();
            CreateMap<VisitingResource, Visiting>();
            CreateMap<DirectionResource, Direction>();
        }
    }
}