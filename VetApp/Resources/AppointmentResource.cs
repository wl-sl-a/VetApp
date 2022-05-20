namespace VetApp.Resources
{
    public class AppointmentResource
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public int DoctorId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TypeOfService { get; set; }
        public string Status { get; set; }
    }
}
