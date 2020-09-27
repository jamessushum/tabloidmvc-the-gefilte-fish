using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: UserProfileController
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetAllActive();
            return View(userProfiles);
        }
        // GET: UserProfileController/Deactivated
        public ActionResult Deactivated()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetDeactivated();
            return View(userProfiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetById(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            ProfileEditViewModel evm = new ProfileEditViewModel()
            {
                User = _userProfileRepository.GetById(id),
                UserTypes = _userProfileRepository.GetUserTypes()
            };
            

            if (evm.User == null)
            {
                return NotFound();
            }

            return View(evm);
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProfileEditViewModel evm)
        {
            try
            {
                _userProfileRepository.Update(evm.User);

                return RedirectToAction("Index");
            }
            catch
            {
                evm.UserTypes =_userProfileRepository.GetUserTypes();
                return View(evm);
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetById(id);
            return View(userProfile);
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserProfile userProfile)
        {
            try
            {
                userProfile.Deactivated = true;
                _userProfileRepository.Update(userProfile);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(userProfile);
            }
        }
        // GET: UserProfileController/Reactivate/5
        public ActionResult Reactivate(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetById(id);
            return View(userProfile);
        }

        // POST: UserProfileController/Reactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(int id, UserProfile userProfile)
        {
            try
            {
                userProfile.Deactivated = false;
                _userProfileRepository.Update(userProfile);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(userProfile);
            }
        }
    }
}
