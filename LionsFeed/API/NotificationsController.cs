using LionsFeed.Controllers;
using LionsFeed.Data;
using LionsFeed.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LionsFeed.API
{
    [Produces("application/json")]
    [Route("api/Notifications")]
    [Authorize]
    public class NotificationsController : BaseController
    {

        private LionsContext _context;

        public NotificationsController(LionsContext context)
        {
            _context = context;

        }

        [Route("GetNotifications")]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var user = GetLoggedInUser();
            var userId = user.Id;

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(n => n.Post.Artist)
                .ToList();




            return notifications.Select(n => new NotificationDto()
            {
                
                DateTime = n.DateTime,
                Post = new PostDto
                {
                    Artist = new UserDto
                    {
                        Id = n.Post.Artist.Id,
                        Name = n.Post.Artist.FirstName + " " + n.Post.Artist.LastName
                    },
                    DateTime = n.Post.CreatedDate,
                    Id = n.Post.ID,

                },
                Type = n.Type,
                UserName = user.FirstName + " " + user.LastName,
                Actor = new UserDto
                {
                    Id = n.ActorId,
                   
                    Name = _context.Users.Where(u => u.Id == n.ActorId).SingleOrDefault().FirstName
                            + " " +
                            _context.Users.Where(u => u.Id == n.ActorId).SingleOrDefault().LastName
                }
            });

            
        }

        [HttpPost]
        [Route("MarkAsRead")]
        public IActionResult MarkAsRead()
        {
            var userId = GetLoggedInUser().Id;

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}