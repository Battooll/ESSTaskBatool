using ESSTaskBatool.Data;
using ESSTaskBatool.ViewModels;

namespace ESSTaskBatool.Repos
{
    public interface IUser
    {
        Task<int> AddUser(Tanent1user user);
        Task<Tanent1user> GetUser(int id);
        Task<List<Tanent1user>> GetAllUsers();
        Task<int> UpdateUser(int id, Tanent1user user);
        Task<int> DeleteUser(int id);
        Task<AuthenticateResult> AuthenticateAsync(LoginViewModel model);
    }
}
