
using DocNotifyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocNotifyAPI.Context
{
    public class DocContext : DbContext
    {
        public DocContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

       public  DbSet<User> Users { get; set; }
        
    } 
}