using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAll(string iden);
        Task<Animal> GetAnimalById(int id, string iden);
        Task<IEnumerable<Animal>> GetAnimalsByOwnerId(int animalId, string iden);
        Task<Animal> CreateAnimal(Animal newAnimal, string iden);
        Task UpdateAnimal(int id, Animal animal);
        Task DeleteAnimal(Animal animal);
    }
}