using LionsFeed.Data;
using LionsFeed.Models;
using LionsFeed.ViewModel.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LionsFeed.Controllers
{
    public class CommentsController : BaseController
    {

        private LionsContext _context;
        public CommentsController(LionsContext context)
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            var post = _context.Posts
                .Include(p => p.Artist)
                .Where(p => p.ID == id).SingleOrDefault();

            var comments = _context.Comments
                .Where(c => c.PostId == post.ID)
                .Include(c => c.User)
                .ToList();

            SetPost(post);

            var model = new CommentsViewModel
            {
                Post = post,
                PostId = id,
                Comments = comments.OrderByDescending(c => c.CommentDate)

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CommentsViewModel model)
        {

            var userId = GetLoggedInUser().Id;

            var post = GetPost();

            var comment = new Comment
            {
                UserId = userId,
                PostId = post.ID,
                CommentDate = DateTime.Now,
                CommentText = model.CommentText
            };

            post.Comments.Add(comment);
            post.CommentsCount++;
            _context.Update(post);


            var notification = new Notification
            {
                Type = NotificationType.PostCommented,
                DateTime = DateTime.Now,
                Post = post,
                ActorId = userId
            };
            _context.Notifications.Add(notification);


            var postCreator = _context.Posts
                .Where(p => p.ID == post.ID)
                .Select(p => p.Artist)
                .SingleOrDefault();

            var userNotification = new UserNotification
            {
                NotificationId = notification.Id,
                UserId = postCreator.Id
            };

            postCreator.UserNotifications.Add(userNotification);
            _context.Update(postCreator);



            _context.SaveChanges();

            var comments = _context.Comments
                .Where(c => c.PostId == post.ID)
                .Include(c => c.User)
                .ToList();

            var viewModel = new CommentsViewModel
            {
                Comments = comments.OrderByDescending(c => c.CommentDate),
                CommentText = null,
                Post = post,
                PostId = post.ID

            };

            return View(viewModel);
        }
    }
}