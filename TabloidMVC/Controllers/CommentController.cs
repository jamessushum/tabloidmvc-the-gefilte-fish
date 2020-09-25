using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        // GET: Comment/Index/1
        public ActionResult Index(int Id)
        {
            Post post = _postRepository.GetPublishedPostById(Id);

            List<Comment> comments = _commentRepository.GetCommentsByPost(Id);

            PostCommentViewModel vm = new PostCommentViewModel()
            {
                Post = post,
                Comments = comments,
                CurrentUserProfileId = GetCurrentUserProfileId()
            };

            return View(vm);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create/1
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int Id, Comment comment)
        {
            try
            {
                comment.PostId = Id;
                comment.UserProfileId = GetCurrentUserProfileId();
                comment.CreateDateTime = DateTime.Now;

                _commentRepository.AddComment(comment);

                return RedirectToAction("Index", new { id = Id });
            }
            catch(Exception ex)
            {
                return View(comment);
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
