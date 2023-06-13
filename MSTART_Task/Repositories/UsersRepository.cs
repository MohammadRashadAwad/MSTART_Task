
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSTART_Task.Enums;
using MSTART_Task.Helper;
using MSTART_Task.Models;
using MSTART_Task.Services;
using MSTART_Task.ViewModels;


namespace MSTART_Task.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notification;

        public UsersRepository(ApplicationDbContext context, INotificationService notification)
        {
            _context = context;
            _notification = notification;

        }

        public async Task<bool> AddNew(UserViewModel model)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username) != null)
            {
                _notification.Warning("The Username is already exist");
                return false;
            }
            if (await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email) != null)
            {
                _notification.Warning("The Email address is already exist");
                return false;
            }
            var user = Mapper.MapUserViewModelToUser(model);
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            _notification.Successfully("User added successfully");

            return true;

        }

        public async Task<bool> Delete(List<int> userIds)
        {
            foreach (var userId in userIds)
            {

                var user = await GetById(userId);
                if (user == null) {
                    _notification.Warning("Something is wrong");
                    return false;
                }
                user.Status = 0;
                var accounts = await _context.Account.Where(u => u.User_ID == userId).ToListAsync();
                if (accounts.Any())
                {
                    foreach (var account in accounts)
                    {
                        account.Status = 0;
                    }
                }
               

            }
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<User>> GetAll(string search, int page, int pageSize)
        {

            var users = await _context.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u =>
                  u.Username.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                  u.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                  u.Id.ToString().Contains(search)
                  ).ToList();
            }
            return users;
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<bool> Update(UserViewModel model, bool continueEditing)
        {
            var existingUser = await GetById(model.Id);

            if (existingUser.Email == model.Email && existingUser.Username == model.Username)
            {
                existingUser.First_Name = model.First_Name;
                existingUser.Last_Name = model.Last_Name;
                existingUser.DateTime_UTC = model.DateTime_UTC;
                existingUser.Date_Of_Birth = model.Date_Of_Birth;
                existingUser.Update_DateTime_UTC = model.Update_DateTime_UTC;
                existingUser.Server_DateTime = model.Server_DateTime;
                existingUser.Gender = (int)model.Gender;
                existingUser.Status = (int)model.Status;

                await _context.SaveChangesAsync();
                if (continueEditing)
                {
                    _notification.Successfully("The information has been saved successfully");
                }
                else
                {
                    _notification.Successfully("Information has been updated and success");
                }


                return true;
            }
            if (await IsUsernameExist(model.Username, model.Id))
            {
                _notification.Warning("The Username already exists");
                return false;
            }
            if (await IsEmailExist(model.Email, model.Id))
            {
                _notification.Warning("The Email address already exists");
                return false;
            }

            var newUser = Mapper.MapUserViewModelToUser(model);
            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
            if (continueEditing)
            {
                _notification.Successfully("The information has been saved successfully");
            }
            else
            {
                _notification.Successfully("The information has been updated successfully");
            }

            return true;
        }

        private async Task<bool> IsUsernameExist(string username, int id)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Id != id);
        }

        private async Task<bool> IsEmailExist(string email, int id)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);
        }





    }
}
