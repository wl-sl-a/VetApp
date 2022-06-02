using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        public ServiceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Service> CreateService(Service newService)
        {
            await unitOfWork.Services
                .AddAsync(newService);
            await unitOfWork.CommitAsync();

            return newService;
        }

        public async Task DeleteService(Service service)
        {
            unitOfWork.Services.Remove(service);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Service>> GetAll(string iden)
        {
            return await unitOfWork.Services.GetAllAsync(iden);
        }

        public async Task<IEnumerable<Service>> Search(string iden, string param)
        {
            return await unitOfWork.Services.Search(iden, param);
        }

        public async Task<Service> GetServiceById(int id, string iden)
        {
            return await unitOfWork.Services.GetByIdAsync(id, iden);
        }

        public async Task UpdateService(int id, Service service)
        {
            service.Id = id;
            unitOfWork.Services.Entry(service);
            await unitOfWork.CommitAsync();
        }
    }
}
