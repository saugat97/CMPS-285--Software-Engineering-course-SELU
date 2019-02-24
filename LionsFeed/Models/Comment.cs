using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LionsFeed.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }

        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        
    }
}
