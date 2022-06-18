using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetApp.Core.Models
{
    public class FilterAppointment
    {
        public int? AnimalId { get; set; }
        public int? DoctorId { get; set; }
        public int? ServiceId { get; set; }
        public string Status { get; set; }
    }
}
