using MSTART_Task.Models;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll(string search, int page, int pageSize);
        Task<Account> GetById(int id);
        Task<bool> AddNew(AccountViewModel model);
        Task<bool> Update(AccountViewModel model, bool continueEditing);
        Task<bool> Delete(List<int> accountIds);
    }
}
