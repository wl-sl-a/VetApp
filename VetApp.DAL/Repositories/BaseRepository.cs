using System;
using System.Collections.Generic;
using System.Text;

namespace VetApp.DAL.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext dbContext;
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
