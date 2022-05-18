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

            // Resource to Domain
            CreateMap<OwnerResource, Owner>();
            CreateMap<AnimalResource, Animal>();
            CreateMap<DoctorResource, Doctor>();
        }
    }
}