using System.ComponentModel.DataAnnotations;

namespace LionsFeed.ViewModel.ApplicationUsers
{
    public class CreateUserViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
