using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.Resources
{
    public class ScheduleResource
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Weekday { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
