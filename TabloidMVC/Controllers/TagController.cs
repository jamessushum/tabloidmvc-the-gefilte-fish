using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepo;
        private readonly IPostRepository _postRepo;        
        public TagController(ITagRepository tagRepository, IPostRepository postRepository)
        {
            _tagRepo = tagRepository;
            _postRepo = postRepository;
        }
        // GET: TagController
        public ActionResult Index()
        {
            List<Tag> tags = _tagRepo.GetAllTags();

            return View(tags);
        }

        // GET: TagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepo.Add(tag);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            try
            {
                _tagRepo.Update(tag);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(tag);
            }
        }

        //GET: TagController
        public ActionResult TagManager(int Id)
        {

            //all tags
            List<Tag> tags = _tagRepo.GetAllTags();
            //current tags that apply to post
            List<Tag> currentTags = _tagRepo.GetPostTags(Id);

            AddTagPostViewModel vm = new AddTagPostViewModel
            {
                Post = _postRepo.GetPublishedPostById(Id),
                Tags = _tagRepo.GetAllTags(),
                CurrentTagIds = new List<int>()
            };

           foreach (Tag tag in currentTags)
            {
                vm.CurrentTagIds.Add(tag.Id);
            }
            
            return View(vm);
        }

        //POST: TagRepository
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TagManager(int id, AddTagPostViewModel vm)
        {
            try
            {
                List<Tag> previouslySelectedTags = _tagRepo.GetPostTags(id);
                List<int> previouslySelectedTagIds = new List<int>();

                foreach (Tag tag in previouslySelectedTags)
                {
                    previouslySelectedTagIds.Add(tag.Id);
                }

                

                if (vm.SelectedTagIds != null)
                {
                    foreach (int tagId in vm.SelectedTagIds)
                    {
                        if (!previouslySelectedTagIds.Contains(tagId))
                        {
                            _tagRepo.AddTagToPost(tagId, id);
                        }
                        
                    }
                    foreach (int tagId in previouslySelectedTagIds)
                    {
                        if (!vm.SelectedTagIds.Contains(tagId))
                        {
                            _tagRepo.RemoveTagFromPost(tagId, id);
                        }
                    }
                }

                return RedirectToAction("Details", "Post", new { id = id });
            }
            catch (Exception)
            {
                return RedirectToAction("Details", "Post", new { id = id });
            }
        }
    }
}
