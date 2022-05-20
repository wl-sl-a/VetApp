namespace VetApp.Resources
{
    public class VisitingResource
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int DoctorId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Diagnosis { get; set; }
        public string Analyzes { get; set; }
        public string Examination { get; set; }
        public string Medicines { get; set; }
    }
}
