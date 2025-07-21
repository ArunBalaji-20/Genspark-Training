
using Microsoft.EntityFrameworkCore;
using videoportalAPI.Models;

namespace videoportalAPI.Context
{
    public class VideosContext : DbContext
    {
        public VideosContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<TrainingVideos> TrainingVideos { get; set; }
       

        


    }
}