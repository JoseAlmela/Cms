using AutoMapper;
using EurovalBusinessLogic.Services.ViewModels;
using EurovalDataAccess.Entities;
using EurovalDataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EurovalBusinessLogic.Services
{
    public class EurovalCmsService : IEurovalCmsService
    {
        private readonly IEurovalCmsRepository _repository;
        private readonly ILogger<EurovalCmsService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public EurovalCmsService(IEurovalCmsRepository repository, ILogger<EurovalCmsService> logger, IMapper mapper)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
        }
        #region Pistas
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PistaViewModel>> GetPistasAsync()
        {
            return _mapper.Map<IEnumerable<Pista>, IEnumerable<PistaViewModel>>
                (await _repository.GetAllPistasAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PistaViewModel> GetPistaAsync(int id)
        {
            return _mapper.Map<Pista, PistaViewModel>
                (await _repository.GetPistaAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> PistaExistsAsync(int id)
        {
            return await _repository.EntityExistsAsync<Pista>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pista"></param>
        /// <returns></returns>
        public async Task<PistaViewModel> UpdatePistaAsync(PistaViewModel pista)
        {

            try
            {
                _repository.ModifyEntity(_mapper.Map<PistaViewModel, Pista>(pista));
                await _repository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                if (! await PistaExistsAsync(pista.Id))
                {
                    _logger.LogError($"Failed to update Pista {pista.Id}. Not found: {e}");
                    return null;
                }
                else
                {
                    _logger.LogError($"Failed to update Pista {pista.Id}.  Unknown Problem: {e}");
                    throw new Exception("Unknown problem with the database", e);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Pista: {ex}");
                return null;
            }

            return pista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pista"></param>
        /// <returns></returns>
        public async Task<PistaViewModel> CreatePistaAsync(PistaViewModel pista)
        {
            try
            {
                Pista p = _mapper.Map<PistaViewModel, Pista>(pista);
                _repository.AddEntity(p);
                await _repository.SaveAllAsync();
                pista.Id = p.Id;
                return pista;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Create Pista: {ex}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemovePistaAsync(int id)
        {
            _repository.RemoveEntity(new Pista { Id = id });
            return await _repository.SaveAllAsync();
        }

        #endregion

        #region Socios

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SocioViewModel>> GetSociosAsync()
        {
            return _mapper.Map<IEnumerable<Socio>, IEnumerable<SocioViewModel>>
                (await _repository.GetAllSociosAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SocioViewModel> GetSocioAsync(int id)
        {
            return _mapper.Map<Socio, SocioViewModel>
                (await _repository.GetSocioAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socio"></param>
        /// <returns></returns>
        public async Task<SocioViewModel> UpdateSocioAsync(SocioViewModel socio)
        {
            try
            {
                _repository.ModifyEntity(_mapper.Map<SocioViewModel, Socio>(socio));
                await _repository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                if (!await SocioExistsAsync(socio.Id))
                {
                    _logger.LogError($"Failed to update Socio {socio.Id}. Not found: {e}");
                    return null;
                }
                else
                {
                    _logger.LogError($"Failed to update Pista {socio.Id}.  Unknown Problem: {e}");
                    throw new Exception("Unknown problem with the database", e);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Pista: {ex}");
                return null;
            }

            return socio;
        }

        public async Task<SocioViewModel> CreateSocioAsync(SocioViewModel socio)
        {
            try
            {
                _repository.AddEntity(socio);
               await _repository.SaveAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Socio: {ex}");
                return null;
            }

            return socio;
        }

        public async Task<bool> RemoveSocioAsync(int id)
        {
            _repository.RemoveEntity(new Socio { Id = id });
            return await _repository.SaveAllAsync();
        }

        public async Task<bool> SocioExistsAsync(int id)
        {
            return await _repository.EntityExistsAsync<Socio>(id);
        }

        #endregion

        #region Reservas
        public async Task<IEnumerable<ReservaViewModel>> GetReservasAsync(bool includeExtraInfo)
        {
            return _mapper.Map<IEnumerable<Reserva>, IEnumerable<ReservaViewModel>>
                (await _repository.GetAllReservasAsync(includeExtraInfo));
        }

        public async Task<ReservaViewModel> GetReservaAsync(int id)
        {
            return _mapper.Map<Reserva, ReservaViewModel>
                (await _repository.GetReservaAsync(id));
        }

        public async Task<ReservaViewModel> UpdateReservaAsync(ReservaViewModel reserva)
        {
            try
            {
                _repository.ModifyEntity(_mapper.Map<ReservaViewModel, Reserva>(reserva));
                await _repository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {

                if (!await ReservaExistsAsync(reserva.Id))
                {
                    _logger.LogError($"Failed to update Reserva {reserva.Id}. Not found: {e}");
                    return null;
                }
                else
                {
                    _logger.LogError($"Failed to update Reserva {reserva.Id}. Unknown Problem: {e}");
                    throw new Exception("Unknown problem with the database", e);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Reserva: {ex}");
                return null;
            }

            return reserva;

        }

        public async Task<ReservaViewModel> CreateReservaAsync(ReservaViewModel reserva)
        {
            try
            {
                Reserva p = _mapper.Map<ReservaViewModel, Reserva>(reserva);
                _repository.AddEntity(p);
                await _repository.SaveAllAsync();
                reserva.Id = p.Id;
                return reserva;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Create Pista: {ex}");
                return null;
            }
        }

        public async Task<bool> RemoveReservaAsync(int id)
        {
                _repository.RemoveEntity(new Reserva { Id = id });
                return await _repository.SaveAllAsync();
        }

        public Task<bool> ReservaExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
