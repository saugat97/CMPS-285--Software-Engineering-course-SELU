using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LionsFeed.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(8, MinimumLength =8, ErrorMessage ="Enter a valid WNumber.")]
        public string WNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string tempPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
