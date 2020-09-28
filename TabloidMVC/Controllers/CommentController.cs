using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            Post post = _postRepository.GetPostById(Id);

            List<Comment> comments = _commentRepository.GetCommentsByPost(Id);

            PostCommentViewModel vm = new PostCommentViewModel()
            {
                Post = post,
                Comments = comments,
                CurrentUserProfileId = GetCurrentUserProfileId()
            };

            return View(vm);
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id);

            return View(comment);
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

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            int currentUserProfileId = GetCurrentUserProfileId();

            Comment comment = _commentRepository.GetCommentById(id);

            if (comment.UserProfileId != currentUserProfileId)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepository.UpdateComment(comment);

                return RedirectToAction("Details", new { id = id });
            }
            catch(Exception ex)
            {
                return View(comment);
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            int currentUserProfileId = GetCurrentUserProfileId();

            Comment comment = _commentRepository.GetCommentById(id);

            if (comment.UserProfileId != currentUserProfileId)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            try
            {
                Comment commentToBeDeleted = _commentRepository.GetCommentById(id);

                _commentRepository.DeleteComment(id);

                return RedirectToAction("Index", new { id = commentToBeDeleted.PostId });
            }
            catch(Exception ex)
            {
                return View(comment);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
