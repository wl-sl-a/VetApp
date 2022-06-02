using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VetApp.Core.Models
{
    public class Service
    {
        public Service()
        {
            Appointments = new Collection<Appointment>();
            Directions = new Collection<Direction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Info { get; set; }
        public string VetName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Direction> Directions { get; set; }
    }
}
