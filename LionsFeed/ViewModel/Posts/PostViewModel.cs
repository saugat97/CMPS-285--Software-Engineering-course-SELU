using LionsFeed.Models;
using System.Collections.Generic;

namespace LionsFeed.ViewModel.Posts
{
    public class PostViewModel
    {
        public string PostText { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public bool IsFlagged { get; set; }

        public string ImageUrl { get; set; }

        public bool IsLiked { get; set; }

    }
}
