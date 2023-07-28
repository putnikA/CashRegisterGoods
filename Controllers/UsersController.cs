using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashRegisterGoods.Data;
using CashRegisterGoods.Models;
using CashRegisterGoods.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CashRegisterGoods.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Perform any logout logic here, such as clearing authentication cookies, etc.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserName,UserPassword")] Users user)
        {
            if (ModelState.IsValid)
            {
                // Logging form data
                Console.WriteLine($"UserName: {user.UserName}, UserPassword: {user.UserPassword}");

                // Redirect the user to the Goods start page
                return RedirectToAction("Index", "Goods");
            }

            // Log validation errors to the console
            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation error for property '{key}': {error.ErrorMessage}");
                }
            }

            // If the model state is not valid, return to the login view with error messages
            return View("Login", user);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login([Bind("UserName,UserPassword")] Users user)
        //{
        //if (ModelState.IsValid)
        //{
        //// Retrieve the user from the database based on the provided username
        //var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);

        //if (dbUser != null)
        //{
        //// Replace this part with your actual password hashing and verification logic
        //if (dbUser.UserPassword == user.UserPassword)
        //{
        //var claims = new List<Claim>
        //{
        //new Claim(ClaimTypes.Name, dbUser.UserName),
        //// Add more claims if needed
        //};

        //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //var principal = new ClaimsPrincipal(identity);

        //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //// Redirect the user to the Goods start page after successful login
        //return RedirectToAction("Index", "Goods");
        //}
        //}

        //// Authentication failed, show error message to the user
        //ModelState.AddModelError(string.Empty, "Invalid credentials");
        //}

        //// If the model state is not valid, return to the login view with error messages
        //return View("Login", user);
        //}

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,UserName,UserPassword,UserEmail")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Goods");
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,UserPassword,UserEmail")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
