using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VetApp.Core.Models
{
    public class Animal
    {
        public Animal()
        {
            Appointments = new Collection<Appointment>();
            Visitings = new Collection<Visiting>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public int Age { get; set; }
        public string Kind { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Visiting> Visitings { get; set; }
    }
}
