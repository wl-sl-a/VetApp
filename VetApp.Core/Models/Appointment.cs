using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetApp.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string Status { get; set; }
    }
}
