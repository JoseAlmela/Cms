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
        public async Task<bool> PistaExists(int id)
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

                if (! await PistaExists(pista.Id))
                {
                    _logger.LogError($"Failed to update Pista {pista.Id}. Not found: {e}");
                    return null;
                }
                else
                {
                    _logger.LogError($"Failed to update Pista {pista.Id}. Not found: {e}");
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
                    _logger.LogError($"Failed to update Pista {socio.Id}. Not found: {e}");
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
    }
}
