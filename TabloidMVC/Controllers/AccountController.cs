using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var userProfile = _userProfileRepository.GetByEmail(credentials.Email);

            if (userProfile == null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            ViewBag.DuplicateEmail = false;
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userProfile)
        {
            try
            {
                //getting userTypes so that usertypeId isn't hard coded
                List<UserType> userTypes = _userProfileRepository.GetUserTypes();
                UserType author = userTypes.First(type => type.Name == "Author");

                //getting all users for simple email verification
                List<UserProfile> allActiveUsers = _userProfileRepository.GetAllActive();
                List<UserProfile> deactivatedUsers = _userProfileRepository.GetDeactivated();

                // checks active users
                foreach (UserProfile user in allActiveUsers)
                {
                    if (user.Email == userProfile.Email)
                    {
                        ViewBag.DuplicateEmail = true;
                        return View();
                    }
                }
                // checks deactivated users
                foreach (UserProfile user in deactivatedUsers)
                {
                    if (user.Email == userProfile.Email)
                    {
                        ViewBag.DuplicateEmail = true;
                        return View();
                    }
                }

                userProfile.CreateDateTime = DateTime.Now;
                userProfile.UserTypeId = author.Id;
                userProfile.Deactivated = false;

                _userProfileRepository.Create(userProfile);

                //Create new credentials for login
                Credentials credentials = new Credentials
                {
                    Email = userProfile.Email
                };

                Login(credentials);

                return RedirectToAction("Index", "Home"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:", ex);
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
