namespace LionsFeed.Models
{
    public class Upvote
    {
        public ApplicationUser UpVoter { get; set; }

        public Post Post { get; set; }


        public int UpvoterId { get; set; }
        
        public int PostId { get; set; }
    }
}
