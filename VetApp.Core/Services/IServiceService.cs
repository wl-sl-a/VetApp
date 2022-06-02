using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAll(string iden);
        Task<Service> GetServiceById(int id, string iden);
        Task<Service> CreateService(Service newService);
        Task UpdateService(int id, Service service);
        Task DeleteService(Service service);
        Task<IEnumerable<Service>> Search(string iden, string param);
    }
}
