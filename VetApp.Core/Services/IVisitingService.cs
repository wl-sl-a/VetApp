using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetApp.Core.Models;

namespace VetApp.Core.Services
{
    public interface IVisitingService
    {
        Task<IEnumerable<Visiting>> GetAll(string iden);
        Task<Visiting> GetVisitingById(int id, string iden);
        Task<IEnumerable<Visiting>> GetVisitingsByAnimalId(int animalId, string iden);
        Task<Visiting> CreateVisiting(Visiting newVisiting, string iden);
        Task UpdateVisiting(int id, Visiting visiting);
        Task DeleteVisiting(Visiting visiting);
        void print(string q, Visiting visiting);
    }
}
