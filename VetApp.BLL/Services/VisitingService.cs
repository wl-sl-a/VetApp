using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class VisitingService : IVisitingService
    {
        private readonly IUnitOfWork unitOfWork;
        public VisitingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Visiting> CreateVisiting(Visiting newVisiting, string iden)
        {
            if (unitOfWork.Visitings.CheckVet(newVisiting.Animal.OwnerId, iden))
            {
                await unitOfWork.Visitings.AddAsync(newVisiting);
                await unitOfWork.CommitAsync();
                return newVisiting;
            }
            return new Visiting();
        }

        public async Task DeleteVisiting(Visiting visiting)
        {
            unitOfWork.Visitings.Remove(visiting);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Visiting>> GetAll(string iden)
        {
            return await unitOfWork.Visitings
                .GetAllAsync(iden);
        }

        public async Task<Visiting> GetVisitingById(int id, string iden)
        {
            return await unitOfWork.Visitings
                .GetVisitingByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Visiting>> GetVisitingsByAnimalId(int animalId, string iden)
        {
            return await unitOfWork.Visitings
                .GetAllByAnimalIdAsync(animalId, iden);
        }

        public async Task UpdateVisiting(int id, Visiting visiting)
        {
            visiting.Id = id;
            unitOfWork.Visitings.Entry(visiting);
            await unitOfWork.CommitAsync();
        }
    }
}
