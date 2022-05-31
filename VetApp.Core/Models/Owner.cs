using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VetApp.Core.Models
{
    public class Owner
    {
        public Owner()
        {
            Animals = new Collection<Animal>();
        }
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string VetName { get; set; }
        public ICollection<Animal> Animals { get; set; }
    }
}
