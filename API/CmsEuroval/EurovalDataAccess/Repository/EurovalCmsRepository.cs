using EurovalDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EurovalDataAccess.Repository
{
    public class EurovalCmsRepository : IEurovalCmsRepository
    {
        private readonly EurovalCmsContext _ctx;
        private readonly ILogger<EurovalCmsContext> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="logger"></param>
        public EurovalCmsRepository(EurovalCmsContext ctx, ILogger<EurovalCmsContext> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
#if DEBUG

            _logger.LogDebug("----------------------Debugging-----------------");
#endif
        }
        #region Common
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void RemoveEntity(object model)
        {
            _ctx.Remove(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void ModifyEntity(object model)
        {
            _ctx.Update(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> EntityExistsAsync<T>(int id) where T : class
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties();
            return await _ctx.Set<T>()
                .AsNoTracking()
                .AnyAsync(item => (int)properties
                                        .Single(p => p.Name == "Id")
                                        .GetValue(item) == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAllAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
        #endregion

#region Pistas

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Pista>> GetAllPistasAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllPistasAsync)} was called");

                return await _ctx.Pistas
                           .AsNoTracking()
                           .OrderBy(p => p.Nombre)
                           .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Pistas: {ex}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pista> GetPistaAsync(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetPistaAsync)} was called");

                return await _ctx.Pistas
                          .AsNoTracking()
                          .SingleOrDefaultAsync(p=>p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Pista: {ex}");
                return null;
            }
        }

        #endregion

        #region Socios
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Socio>> GetAllSociosAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllSociosAsync)} was called");

                return await _ctx.Socios
                           .AsNoTracking()
                           .OrderBy(p => p.Nombre)
                           .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Socios: {ex}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Socio> GetSocioAsync(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetSocioAsync)} was called");

                return await _ctx.Socios
                          .AsNoTracking()
                          .SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Socio: {ex}");
                return null;
            }
        }

        public async Task<IEnumerable<Reserva>> GetAllReservasAsync(bool includeExtraInfo)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllReservasAsync)} was called");
                var q = _ctx.Reservas
                           .AsNoTracking()
                           .OrderBy(p => p.FechaReserva);
                if (includeExtraInfo)
                {
                    q.Include(r => r.Pista);
                    q.Include(r => r.Socio);

                }
                return await q                          
                           .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Reserva: {ex}");
                return null;
            }
        }

        public async Task<Reserva> GetReservaAsync(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetReservaAsync)} was called");

                return await _ctx.Reservas
                          .AsNoTracking()
                          .SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Rerserva: {ex}");
                return null;
            }
        }
        #endregion
    }
}
