using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll(string iden);
        Task<Owner> GetOwnerById(int id, string iden);
        Task<Owner> CreateOwner(Owner newOwner);
        Task UpdateOwner(int id, Owner owner);
        Task DeleteOwner(Owner owner);
        Owner GetOwnerByUsername(string username);
        Task<IEnumerable<Owner>> Search(string iden, string param);
    }
}
