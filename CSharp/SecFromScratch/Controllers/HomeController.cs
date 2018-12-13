using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecFromScratch.Models;
using SecFromScratch.Services;

namespace SecFromScratch.Controllers {

    public class HomeController : Controller {
        private readonly AppDbContext _context;

        public HomeController (AppDbContext context) {
            _context = context;
        }
        public IActionResult Index () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (User user) {
            if (ModelState.IsValid) {
                user.Password = ManualAuth.Sha256 (user.Password);
                _context.Add (user);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Welcome));
            }
            return View (user);
        }

        public IActionResult Welcome () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (User user) {

            if (ModelState.IsValid) {
                // attempt to get a user with the matching username from DB.
                User GetUser = await _context.Users.SingleOrDefaultAsync (u => u.UserName == user.UserName);
                // if no match on username skip password check.
                if (GetUser != null) {
                    // compare hashed passwords.
                    if (ManualAuth.Sha256Check (user.Password, GetUser.Password)) {
                        // if password match is true return treats.
                        return View ("Treats");
                    }
                }
            }
            return View ("LoginFail");
        }

        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}