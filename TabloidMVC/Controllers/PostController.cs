using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _urseRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPostById(id);
            
                if (post == null)return NotFound();
            
            return View(post);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }
        
        public IActionResult Delete(int id)
        {
            Post post = _postRepository.GetPostById(id);
            

            return View(post);
        }
        [HttpPost]
        public IActionResult Delete(int id, Post post)
        {
            try 
            {
                _postRepository.DeletePost(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("UserPosts");
        }
        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        public IActionResult Edit(int id)
        {
            HttpGetAttribute http = new HttpGetAttribute();
            Console.WriteLine(http.Name);
            PostCreateViewModel vm = new PostCreateViewModel()
            {
                Post = _postRepository.GetPostById(id),
                CategoryOptions = _categoryRepository.GetAll()
            };
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, Post post)
        {
            Console.WriteLine("Hitting Edit P2");
            post.Print();
            PostCreateViewModel vm = new PostCreateViewModel()
            {
                Post = _postRepository.GetPostById(id),
                CategoryOptions = _categoryRepository.GetAll()
            };
            try
            {
                _postRepository.EditPost(post);
                return RedirectToAction("UserPosts");
            }
            catch(Exception ex)
            {
                return View(vm);
            }
        }
        public IActionResult UserPosts(int id)
        {
            try
            {
                List<Post> posts = _postRepository.GetUserPosts(GetCurrentUserProfileId());
                ViewBag.Length = posts.Count;
                return View(posts);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
