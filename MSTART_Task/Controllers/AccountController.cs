using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSTART_Task.Helper;
using MSTART_Task.Models;
using MSTART_Task.Repositories;
using MSTART_Task.ViewModels;

namespace MSTART_Task.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _repository;
        private readonly ApplicationDbContext _context;

        public AccountController(IAccountRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }


        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 5)
        {
            var accounts = await _repository.GetAll(search, page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = await _context.Account.CountAsync();
            return View(accounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNew(AccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);
            await _repository.AddNew(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var account = await _repository.GetById(id);
            return account is null ? BadRequest("Invalid User Id") : View(account);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var account = await _repository.GetById(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountViewModel = Mapper.MapAccountToAccountViewModel(account);
            return View(accountViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(List<int> accountIds)
        {
            await _repository.Delete(accountIds);
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Edit(AccountViewModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {

                await _repository.Update(model, continueEditing);

                if (!continueEditing)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


    }
}
