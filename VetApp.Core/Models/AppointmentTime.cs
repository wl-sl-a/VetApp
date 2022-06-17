using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetApp.Core.Models
{
    public class AppointmentTime
    {
        public int Id { get; set; }
        public String Time { get; set; }
        public String Status { get; set; }
        public int Appointment { get; set; }
    }
}
