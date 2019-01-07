using System.Collections.Generic;
using System.Threading.Tasks;
using EurovalBusinessLogic.Services.ViewModels;
using EurovalDataAccess.Entities;

namespace EurovalBusinessLogic.Services
{
    public interface IEurovalCmsService
    {
        PistaViewModel CreatePista(PistaViewModel pista);
        Task<IEnumerable<PistaViewModel>> GetPistasAsync();
        Task<PistaViewModel> GetPistaAsync(int id);
        Task<bool> PistaExists(int id);
        Task<PistaViewModel> UpdatePistaAsync(PistaViewModel pista);
        Task<PistaViewModel> CreatePistaAsync(PistaViewModel pista);
        Task<bool> RemovePistaAsync(int id);
    }
}