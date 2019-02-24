using CryptoHelper;
using LionsFeed.Data;
using LionsFeed.Models;
using LionsFeed.ViewModel.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LionsFeed.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ApplicationUsersController : Controller
    {
        private readonly LionsContext _context;

        public ApplicationUsersController(LionsContext context)
        {
            _context = context;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = Crypto.HashPassword(model.Password),
                    Role = model.Role,
                    Gender = model.Gender
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Name = applicationUser.FirstName + " " + applicationUser.LastName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber,
                Gender = applicationUser.Gender,
                Role = applicationUser.Role
            };
            return View(model);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var words = model.Name.Split(" ");
                var user = _context.Users.Where(u => u.Id == id).SingleOrDefault();

                if (user != null)
                {

                    user.FirstName = words[0];
                    user.LastName = words[1];
                    user.PhoneNumber = model.PhoneNumber;
                    user.Gender = model.Gender;
                    user.Role = model.Role;

                }



                _context.Update(user);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
