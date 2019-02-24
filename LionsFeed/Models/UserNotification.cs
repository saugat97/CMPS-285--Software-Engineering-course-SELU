namespace LionsFeed.Models
{
    public class UserNotification
    {

        public ApplicationUser User { get; set; }

        public int UserId { get; set; }

        public Notification Notification { get; set; }

        public int NotificationId { get; set; }

        
        public bool IsRead { get; set; }

        public void Read() {
            IsRead = true;
        }
    }
}
