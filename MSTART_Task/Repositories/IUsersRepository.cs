using Microsoft.AspNetCore.Mvc;
using MSTART_Task.Models;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Repositories
{
    public interface IUsersRepository
    {
       Task<IEnumerable<User>>GetAll(string search, int page , int pageSize);
       Task<User> GetById(int id);
       Task<bool> Delete (List<int> userIds);
       Task<bool> AddNew(UserViewModel model);
       Task<bool> Update (UserViewModel model, bool continueEditing);   
       


    }
}
