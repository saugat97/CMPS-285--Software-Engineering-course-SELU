using LionsFeed.Models;
using System.ComponentModel.DataAnnotations;

namespace LionsFeed.ViewModel.Manage
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {

        }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        //public string StatusMessage { get; set; }

        public string ImageUrl { get; set; }

        public string LoggedInUserEmail { get; set; }

        public ProfileViewModel(ApplicationUser user, ApplicationUser userLog)
        {
            this.Name = user.FirstName + " " + user.LastName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.ImageUrl = user.ImageUrl;
            this.LoggedInUserEmail = userLog.Email;
        }
    }
}
