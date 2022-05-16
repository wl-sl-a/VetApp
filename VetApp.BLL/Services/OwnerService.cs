using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        public OwnerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Owner> CreateOwner(Owner newOwner)
        {
            await unitOfWork.Owners
                .AddAsync(newOwner);
            await unitOfWork.CommitAsync();

            return newOwner;
        }

        public async Task DeleteOwner(Owner owner)
        {
            unitOfWork.Owners.Remove(owner);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Owner>> GetAll(string iden)
        {
            return await unitOfWork.Owners.GetAllAsync(iden);
        }

        public async Task<IEnumerable<Owner>> Search(string iden, string param)
        {
            return await unitOfWork.Owners.Search(iden, param);
        }

        public async Task<Owner> GetOwnerById(int id, string iden)
        {
            return await unitOfWork.Owners.GetByIdAsync(id, iden);
        }

        public async Task UpdateOwner(int id, Owner owner)
        {
            owner.Id = id;
            unitOfWork.Owners.Entry(owner);
            await unitOfWork.CommitAsync();
        }
    }
}
