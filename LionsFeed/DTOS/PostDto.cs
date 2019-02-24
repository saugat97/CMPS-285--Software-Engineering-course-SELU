using System;

namespace LionsFeed.DTOS
{
    public class PostDto
    {
        public int Id { get; set; }

        public int LikeCount { get; set; }

        public UserDto Artist { get; set; }

        public DateTime DateTime { get; set; }
    }
}
