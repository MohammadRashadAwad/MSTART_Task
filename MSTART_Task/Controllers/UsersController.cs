using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSTART_Task.Models;
using MSTART_Task.Repositories;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Controllers
{
    public class UsersController : Controller

    {
        private readonly IUsersRepository _repository;
        private readonly ApplicationDbContext _context;

        public UsersController(IUsersRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 5)
        {
            var users = await _repository.GetAll(search, page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = await _context.Users.CountAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _repository.GetById(id);
            return user is null ? BadRequest("Invalid User Id") : View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(List<int> userIds)
        {
            await _repository.Delete(userIds);
            return RedirectToAction("Index");
        }



        public IActionResult AddNew()
        {
            return PartialView("_AddNewUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNew(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _repository.AddNew(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Edit
        public async Task<IActionResult> Edit(int id)
        {


            var user = await _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
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
                Status = (Enums.UserStatus)user.Status,

            };

            return View(userViewModel);
        }

        // POST: User/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel model, bool continueEditing)
        {


            if (ModelState.IsValid)
            {

                await _repository.Update(model);

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        //[HttpPost]
        //public IActionResult DeleteUsers(List<int> userIds)
        //{
        //    if (userIds == null || userIds.Count == 0)
        //    {
        //        ModelState.AddModelError("", "No user IDs provided for deletion.");
        //        return RedirectToAction("ViewUsers");
        //    }

        //    // Retrieve the users to be deleted
        //    List<User> usersToDelete = _userRepository.GetUsersByIds(userIds);

        //    if (usersToDelete == null || usersToDelete.Count == 0)
        //    {
        //        ModelState.AddModelError("", "No users found with the provided IDs.");
        //        return RedirectToAction("ViewUsers");
        //    }

        //    // Set the status of users to "Deleted"
        //    foreach (User user in usersToDelete)
        //    {
        //        user.Status = UserStatus.Deleted;
        //    }

        //    // Retrieve associated accounts for the deleted users
        //    List<Account> accountsToDelete = _accountRepository.GetAccountsByUserIds(userIds);

        //    // Set the status of associated accounts to "Deleted"
        //    foreach (Account account in accountsToDelete)
        //    {
        //        account.Status = AccountStatus.Deleted;
        //    }

        //    // Save the changes to the database
        //    _userRepository.SaveChanges();
        //    _accountRepository.SaveChanges();

        //    return RedirectToAction("ViewUsers");
        //}

    }
}
