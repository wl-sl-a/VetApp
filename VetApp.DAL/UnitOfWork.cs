using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Repositories;
using VetApp.DAL.Repositories;

namespace VetApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private AnimalRepository animalRepository;
        private OwnerRepository ownerRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IAnimalRepository Animals => animalRepository = animalRepository ?? new AnimalRepository(context);

        public IOwnerRepository Owners => ownerRepository = ownerRepository ?? new OwnerRepository(context);

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
