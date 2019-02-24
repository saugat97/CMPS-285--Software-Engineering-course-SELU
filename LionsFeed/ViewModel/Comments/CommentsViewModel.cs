using LionsFeed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LionsFeed.ViewModel.Comments
{
    public class CommentsViewModel
    {
        public Post Post { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }

        public string CommentText { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
