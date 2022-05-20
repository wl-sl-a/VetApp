using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VetApp.Core.Models
{
    public class Visiting
    {
        public Visiting()
        {
            Directions = new Collection<Direction>();
        }
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Diagnosis { get; set; }
        public string Analyzes { get; set; }
        public string Examination { get; set; }
        public string Medicines { get; set; }
        public ICollection<Direction> Directions { get; set; }
    }
}
