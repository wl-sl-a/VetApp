using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork unitOfWork;
        public AnimalService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Animal> CreateAnimal(Animal newAnimal, string iden)
        {
            if (unitOfWork.Animals.CheckVet(newAnimal.OwnerId, iden))
            {
                await unitOfWork.Animals.AddAsync(newAnimal);
                await unitOfWork.CommitAsync();
                return newAnimal;
            }
            return new Animal();
        }

        public async Task DeleteAnimal(Animal animal)
        {
            unitOfWork.Animals.Remove(animal);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Animal>> GetAll(string iden)
        {
            return await unitOfWork.Animals
                .GetAllAsync(iden);
        }

        public async Task<Animal> GetAnimalById(int id, string iden)
        {
            return await unitOfWork.Animals
                .GetAnimalByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByOwnerId(int animalId, string iden)
        {
            return await unitOfWork.Animals
                .GetAllByOwnerIdAsync(animalId, iden);
        }

        public async Task UpdateAnimal(int id, Animal animal)
        {
            animal.Id = id;
            unitOfWork.Animals.Entry(animal);
            await unitOfWork.CommitAsync();
        }
    }
}
