using LionsFeed.Controllers;
using LionsFeed.Data;
using LionsFeed.DTOS;
using LionsFeed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LionsFeed.API
{

    [Produces("application/json")]
    [Route("api/Upvotes")]
    public class UpvotesController : BaseController
    {

        private LionsContext _context;

        public UpvotesController(LionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Upvote")]
        public IActionResult Upvote(UpvoteDto dto)
        {
            var userId = GetLoggedInUser().Id;

            if (_context.Upvotes.Any(a => a.UpvoterId == userId && a.PostId == dto.PostId))
                return BadRequest("The upvote already exists.");

            var post = _context.Posts
                .Include(p=>p.Artist)
                .Single(p => p.ID == dto.PostId);
            post.LikeCount++;
            //post.IsLiked = true;
            _context.Posts.Update(post);

            var upvote = new Upvote
            {
                UpvoterId = userId,
                PostId = dto.PostId
            };
            _context.Upvotes.Add(upvote);

            var notification = new Notification
            {
                Type = NotificationType.PostLiked,
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

            var postDto = new PostDto
            {
                Id = post.ID,
                LikeCount = post.LikeCount
            };

            return Ok(postDto);
        }

        [HttpPost]
        [Route("Downvote")]
        public IActionResult Downvote(UpvoteDto dto)
        {
            var userId = GetLoggedInUser().Id;

            var upvoteToRemove = _context.Upvotes.Single(a => a.UpvoterId == userId && a.PostId == dto.PostId);
            _context.Upvotes.Remove(upvoteToRemove);

            var post = _context.Posts.Single(p => p.ID == dto.PostId);
            post.LikeCount--;
            //post.IsLiked = false;

            _context.Posts.Update(post);
            _context.SaveChanges();

            return Ok(post);
        }
    }
}