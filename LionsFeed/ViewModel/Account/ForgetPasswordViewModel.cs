using System.ComponentModel.DataAnnotations;

namespace LionsFeed.ViewModel.Account
{
    public class ForgetPasswordViewModel
    {
        //[Required]
        //public string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Invalid W Number")]
        public string WNumber { get; set; }

    }
}
