﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace VetApp.Core.Services
{
    public interface IDirectionService
    {
        Task<IEnumerable<Direction>> GetAll(string iden);
        Task<Direction> GetDirectionById(int id, string iden);
        Task<IEnumerable<Direction>> GetDirectionsByAnimalId(int animalId, string iden);
        Task<IEnumerable<Direction>> GetDirectionsByVisitingId(int visitingId, string iden);
        Task<Direction> CreateDirection(Direction newDirection, string iden);
        Task UpdateDirection(int id, Direction direction);
        Task DeleteDirection(Direction direction);

        void print(string q, Direction direction, string iden);
    }
}
