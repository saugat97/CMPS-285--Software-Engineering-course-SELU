using System.ComponentModel.DataAnnotations;

namespace LionsFeed.Models
{
    public class Flag
    {
        public int ID { get; set; }
        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
