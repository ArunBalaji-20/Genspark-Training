using TwitterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TwitterAPI.Contexts
{
    public class TwitterContext : DbContext
    {

        public TwitterContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }

        public DbSet<Tweets> Tweets { get; set; }

        public DbSet<TweetLikes> TweetLikes { get; set; }

        public DbSet<Hashtags> Hashtags { get; set; }

        public DbSet<Followings> Followings { get; set; }
        
        public DbSet<FilterByHashtag> FilterByHashtags { get; set; }
    }
}