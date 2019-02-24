using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace LionsFeed.Models
{
    public class Post
    {
        public int ID { get; set; }

        public string PostText { get; set; }

        public DateTime CreatedDate { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public int ArtistId { get; set; }

        public int LikeCount { get; set; }

        public int FlagCount { get; set; }

        public string ImageUrl { get; set; }

        public bool IsLiked { get; set; }

        public bool IsFlagged { get; set; }

        public int CommentsCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new Collection<Comment>();
        }

    }
}

