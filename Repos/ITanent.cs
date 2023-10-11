using ESSTaskBatool.Data;
using ESSTaskBatool.ViewModels;

namespace ESSTaskBatool.Repos
{
    public interface ITanent
    {
        Task<int> AddTanent(RegisterViewModel tanent);
        Task<Tanent1info> GetTanent(int id);
        Task<List<Tanent1info>> GetAllTanent();
        Task<int> UpdateTanent(int id, Tanent1info tanent);
        Task<int> DeleteTanent(int id);
        Task<RegistrationResult> RegisterAsync(RegisterViewModel model);

    }
}
