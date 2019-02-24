using LionsFeed.Models;
using System;

namespace LionsFeed.DTOS
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public PostDto Post { get; set; }

        public string UserName { get; set; }

        public UserDto Actor { get; set; }

    }
}
