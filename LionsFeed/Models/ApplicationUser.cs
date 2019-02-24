using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LionsFeed.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Role { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            UserNotifications = new Collection<UserNotification>();
        }
    }
}
