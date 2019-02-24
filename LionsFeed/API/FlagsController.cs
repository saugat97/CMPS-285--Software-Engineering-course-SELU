using LionsFeed.Controllers;
using LionsFeed.Data;
using LionsFeed.DTOS;
using LionsFeed.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LionsFeed.API
{
    [Produces("application/json")]
    [Route("api/Flags")]
    public class FlagsController : BaseController
    {
        private LionsContext _context;

        public FlagsController(LionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Flag(FlagDto dto)
        {

            var userId = GetLoggedInUser().Id;

            if (_context.Flags.Any(a => a.UserId == userId && a.PostId == dto.PostId))
                return BadRequest("The report already exists.");

            var post = _context.Posts.Single(p => p.ID == dto.PostId);
            post.FlagCount++;
            //checkCountAndSendEmail(post);

            var flag = new Flag
            {
                UserId = userId,
                PostId = dto.PostId
            };

            _context.Flags.Add(flag);
            _context.Posts.Update(post);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult UnFlag(FlagDto dto)
        {

            var userId = GetLoggedInUser().Id;

            var flag = _context.Flags.Single(f => f.PostId == dto.PostId && f.UserId == userId);

            var post = _context.Posts.Single(p => p.ID == flag.PostId);
            post.FlagCount--;

            _context.Flags.Remove(flag);
            _context.Posts.Update(post);
            _context.SaveChanges();

            return Ok();
        }

        public void checkCountAndSendEmail(Post post)
        {
            if(post.FlagCount== 2)
            {
                var notification = new Notification
                {
                    Post = post,
                    DateTime = DateTime.Now,
                    ActorId = post.Artist.Id,
                    Type = NotificationType.PostReportedFirst
                    
                };
                _context.Notifications.Add(notification);

                var admins = _context.Users
                    .Where(u => u.Role == "Admin")
                    .ToList();

                foreach(var admin in admins)
                {
                    var userNotification = new UserNotification
                    {
                        NotificationId = notification.Id,
                        UserId = admin.Id
                    };
                    admin.UserNotifications.Add(userNotification);
                    _context.Update(admin);
                }

                _context.SaveChanges();

            }

            if (post.FlagCount == 3)
            {
                var notification = new Notification
                {
                    Post = post,
                    DateTime = DateTime.Now,
                    ActorId = post.Artist.Id,
                    Type = NotificationType.PostReportedSecond

                };
                _context.Notifications.Add(notification);

                var admins = _context.Users
                    .Where(u => u.Role == "Admin")
                    .ToList();

                foreach (var admin in admins)
                {
                    var userNotification = new UserNotification
                    {
                        NotificationId = notification.Id,
                        UserId = admin.Id
                    };
                    admin.UserNotifications.Add(userNotification);
                    _context.Update(admin);
                }

                _context.SaveChanges();

            }
        }
    }
}