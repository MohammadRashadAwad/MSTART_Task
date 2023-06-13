using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSTART_Task.Helper;
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
        [HttpGet]
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

     
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = Mapper.MapUserToUserViewModel(user);
            return View(userViewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model, bool continueEditing)
        {


            if (ModelState.IsValid)
            {

                await _repository.Update(model,continueEditing);

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

       

    }
}
