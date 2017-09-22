using AssignmentManager.DataAccess;
using AssignmentManager.Entities;
using AssignmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentManager.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/
        public ActionResult Index(int id)
        {
            var commentsRepository = new CommentsRepository();
            var commentsList = new List<Comment>();

            var commentsFromRepository = commentsRepository
                .GetAll(comment => comment.AssignmentId == id);

            var model = new CommentListViewModel();

            //foreach (var comment in commentsFromRepository)
            //{
            //    if (comment.AssignmentId == id)
            //    {
            //        commentsList.Add(comment);
            //    }
            //}

            model.AssignmentId = id;

            var assignmentRepository = new AssignmentRepository();
            model.AssignmentTitle =
                assignmentRepository.GetById(id).Title;

            // Navigational Property Demo

            //var firstComment = commentsList.FirstOrDefault();
            //ViewBag.AssignmentTitle = firstComment.Assignment.Title;

            foreach (var entity in commentsFromRepository)
            {
                var commentViewModel = new CommentViewModel()
                {
                    Id = entity.Id,
                    Content = entity.Content,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    AssignmentId = entity.AssignmentId
                };

                model.Comments.Add(commentViewModel);
            }

            commentsList.AddRange(commentsFromRepository);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var model = new CommentViewModel() 
            { 
                AssignmentId = id
            };

            //entity.AssignmentId = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var entity = new Comment();
            entity.AssignmentId = model.AssignmentId;
            entity.Content = model.Content;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            var commentsRepository = new CommentsRepository();
            commentsRepository.Insert(entity);

            return RedirectToAction("Index/" + entity.AssignmentId);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var commentsRepository = new CommentsRepository();
            var entity = commentsRepository.GetById(id);

            var model = new CommentViewModel() 
            {
                Id = entity.Id,
                Content = entity.Content,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                AssignmentId = entity.AssignmentId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var entity = new Comment();
            entity.Id = model.Id;
            entity.Content = model.Content;
            entity.CreatedAt = model.CreatedAt;
            entity.AssignmentId = model.AssignmentId;
            entity.UpdatedAt = DateTime.Now;

            var commentsRepository = new CommentsRepository();
            commentsRepository.Update(entity);
            
            return RedirectToAction("Index/" + entity.AssignmentId);
        }
        
        public ActionResult Delete(int id)
        {
            var commentsRepository = new CommentsRepository();
            var entity = commentsRepository.GetById(id);
            commentsRepository.Delete(entity);

            return RedirectToAction("Index" + entity.AssignmentId);
        }
	}
}