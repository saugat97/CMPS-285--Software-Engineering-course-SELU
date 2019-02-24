using System;
using System.ComponentModel.DataAnnotations;

namespace LionsFeed.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        [Required]
        public Post Post { get; set; }

        public int ActorId { get; set; }
    }
}
