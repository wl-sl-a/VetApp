using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VetApp.Core.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Appointments = new Collection<Appointment>();
            Visitings = new Collection<Visiting>();
            Schedules = new Collection<Schedule>();
        }
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Specialty { get; set; }
        public string VetName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Visiting> Visitings { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
