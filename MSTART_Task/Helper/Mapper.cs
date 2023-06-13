using MSTART_Task.Models;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Helper
{
    public static class Mapper
    {
        public static Account MapAccountViewModelToAccount(AccountViewModel model)
        {
            return new Account
            {
                User_ID = model.User_ID,
                Account_Number = model.Account_Number,
                Server_DateTime = model.Server_DateTime,
                Update_DateTime_UTC = model.Update_DateTime_UTC,
                DateTime_UTC = model.DateTime_UTC,
                Balance = model.Balance,
                Currency = model.Currency,
                Status = (int)model.Status
            };
        }

        public static User MapUserViewModelToUser(UserViewModel model)
        {
            return new User
            {
                Id = model.Id,
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Username = model.Username,
                Email = model.Email,
                DateTime_UTC = model.DateTime_UTC,
                Date_Of_Birth = model.Date_Of_Birth,
                Update_DateTime_UTC = model.Update_DateTime_UTC,
                Server_DateTime = model.Server_DateTime,
                Gender = (int)model.Gender,
                Status = (int)model.Status,
            };
        }

        public static AccountViewModel MapAccountToAccountViewModel(Account model)
        {
            return new AccountViewModel
            {
                ID = model.ID,
                User_ID = model.User_ID,
                Account_Number = model.Account_Number,
                Balance = model.Balance,
                Currency = model.Currency,
                DateTime_UTC = model.DateTime_UTC,
                Server_DateTime = model.Server_DateTime,
                Status = (Enums.Status)model.Status,
                Update_DateTime_UTC = model.Update_DateTime_UTC
            };
        }

        public static UserViewModel MapUserToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Username = user.Username,
                Email = user.Email,
                DateTime_UTC = user.DateTime_UTC,
                Date_Of_Birth = user.Date_Of_Birth,
                Update_DateTime_UTC = user.Update_DateTime_UTC,
                Server_DateTime = user.Server_DateTime,
                Gender = (Enums.Gender)user.Gender,
                Status = (Enums.Status)user.Status,

            };
        }
    }
}
