using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly IUnitOfWork unitOfWork;
        public DirectionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Direction> CreateDirection(Direction newDirection, string iden)
        {
            newDirection.Visiting = await unitOfWork.Visitings.GetVisitingByIdAsync(newDirection.VisitingId, iden);
            if (newDirection.Visiting != null)
            {
                newDirection.Visiting.Animal = await unitOfWork.Animals.GetAnimalByIdAsync(newDirection.Visiting.AnimalId, iden);
            }
            if (newDirection.Visiting.Animal != null)
            {
                if (unitOfWork.Directions.CheckVet(newDirection.Visiting.Animal.OwnerId, iden))
                {
                    await unitOfWork.Directions.AddAsync(newDirection);
                    await unitOfWork.CommitAsync();
                    return newDirection;
                }
            }
            return new Direction();
        }

        public async Task DeleteDirection(Direction direction)
        {
            unitOfWork.Directions.Remove(direction);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Direction>> GetAll(string iden)
        {
            return await unitOfWork.Directions
                .GetAllAsync(iden);
        }

        public async Task<Direction> GetDirectionById(int id, string iden)
        {
            return await unitOfWork.Directions
                .GetDirectionByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Direction>> GetDirectionsByAnimalId(int visitingId, string iden)
        {
            return await unitOfWork.Directions
                .GetAllByVisitingIdAsync(visitingId, iden);
        }

        public async Task UpdateDirection(int id, Direction direction)
        {
            direction.Id = id;
            unitOfWork.Directions.Entry(direction);
            await unitOfWork.CommitAsync();
        }
    }
}
