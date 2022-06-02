using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetApp.Core.Models
{
    public class Direction
    {
        public int Id { get; set; }
        public int VisitingId { get; set; }
        public Visiting Visiting { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
