using Microsoft.EntityFrameworkCore;
using MSTART_Task.Helper;
using MSTART_Task.Models;
using MSTART_Task.Services;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notification;
        public AccountRepository(ApplicationDbContext context, INotificationService notification)
        {
            _context = context;
            _notification = notification;
        }

        public async Task<bool> AddNew(AccountViewModel model)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == model.User_ID))
            {
                _notification.Warning("Invalid User ID");
                return false;
            }

            if (await _context.Account.AnyAsync(c => c.Account_Number == model.Account_Number))
            {
                _notification.Warning("The Account Number is already exists  ");
                return false;
            }
            var account = Mapper.MapAccountViewModelToAccount(model);
            await _context.AddAsync(account);
            _context.SaveChanges();

            _notification.Successfully("Account added successfully");
            return true;
        }

        public async Task<bool> Delete(List<int> accountIds)
        {
            foreach (var accountId in accountIds)
            {

                var account = await GetById(accountId);
                if (account == null)
                {
                    _notification.Warning("Something is wrong");
                    return false;
                }
                account.Status = 0;
            }
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Account>> GetAll(string search, int page, int pageSize)
        {

            var accounts = await _context.Account.Include(c => c.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(a =>
                  a.User_ID.ToString() == search ||
                  a.ID.ToString() == search ||
                  a.Account_Number == search
                  ).ToList();
            }
            return accounts;
        }

        public async Task<Account> GetById(int id)
        {
            return await _context.Account.Include(c => c.User).FirstOrDefaultAsync(a => a.ID == id);

        }

        public async Task<bool> Update(AccountViewModel model, bool continueEditing)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == model.User_ID))
            {
                _notification.Warning("Invalid User ID");
                return false;
            }

            var account = await GetById(model.ID);

            if (account.Account_Number == model.Account_Number)
            {

                account.User_ID = model.User_ID;
                account.Account_Number = model.Account_Number;
                account.DateTime_UTC = model.DateTime_UTC;
                account.Update_DateTime_UTC = model.Update_DateTime_UTC;
                account.Server_DateTime = model.Server_DateTime;
                account.Status = (int)model.Status;
                account.Currency = model.Currency;
                account.Balance = model.Balance;

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
            if (await _context.Account.AnyAsync(c => c.Account_Number == model.Account_Number))
            {
                _notification.Warning("The Account Number is already exists  ");
                return false;
            }

            var updateAccount = Mapper.MapAccountViewModelToAccount(model);
            _context.Account.Update(updateAccount);
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




    }
}
