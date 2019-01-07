﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EurovalDataAccess.Entities;

namespace EurovalDataAccess.Repository
{
    public interface IEurovalCmsRepository
    {
        void AddEntity(object model);
        Task<bool> EntityExistsAsync<T>(int id) where T : class;
        void ModifyEntity(object model);
        void RemoveEntity(object model);
        Task<bool> SaveAllAsync();

        Task<IEnumerable<Pista>> GetAllPistasAsync();
        Task<Pista> GetPistaAsync(int id);
    }
}