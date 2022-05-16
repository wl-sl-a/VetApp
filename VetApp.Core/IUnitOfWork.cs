﻿using System;
using System.Threading.Tasks;
using VetApp.Core.Repositories;

namespace VetApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IOwnerRepository Owners { get; }
        IAnimalRepository Animals { get; }
        Task<int> CommitAsync();
    }
}

