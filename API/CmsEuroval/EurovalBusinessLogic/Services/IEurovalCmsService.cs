using System.Collections.Generic;
using System.Threading.Tasks;
using EurovalBusinessLogic.Services.ViewModels;
using EurovalDataAccess.Entities;

namespace EurovalBusinessLogic.Services
{
    public interface IEurovalCmsService
    {
        Task<IEnumerable<PistaViewModel>> GetPistasAsync();
        Task<PistaViewModel> GetPistaAsync(int id);
        Task<bool> PistaExists(int id);
        Task<PistaViewModel> UpdatePistaAsync(PistaViewModel pista);
        Task<PistaViewModel> CreatePistaAsync(PistaViewModel pista);
        Task<bool> RemovePistaAsync(int id);
        Task<IEnumerable<SocioViewModel>> GetSociosAsync();
        Task<SocioViewModel> GetSocioAsync(int id);
        Task<SocioViewModel> UpdateSocioAsync(SocioViewModel socio);
        Task<SocioViewModel> CreateSocioAsync(SocioViewModel socio);
        Task<bool> RemoveSocioAsync(int id);
        Task<bool> SocioExistsAsync(int id);
    }
}