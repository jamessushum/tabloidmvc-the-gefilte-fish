using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;

        public CategoryController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Details/5

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 14) throw new ArgumentException("'Other' cannot be deleted.", "id");

                Category category = _categoryRepository.GetCategoryById(id);
                return View(category);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("OtherError");
            }

        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                if (id != 14)
                {
                    // Update posts that are linked to the category to be deleted
                    List<Post> postsToUpdate = _postRepository.GetPostsByCategory(id);
                    foreach (Post post in postsToUpdate)
                    {
                        // ID 14 = "Other"
                        post.CategoryId = 14;
                        _postRepository.EditPost(post);
                    }
                    _categoryRepository.Delete(id);
                }
                else
                {
                    throw new ArgumentException("'Other' cannot be deleted.", "id");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(category);
            }
        }

        public ActionResult OtherError()
        {
            return View();
        }
    }
}
