using Microsoft.EntityFrameworkCore;
using TextMark.Models;



namespace TextMark.Data
{
    public class TextMarkContext : DbContext
    {
        public TextMarkContext(DbContextOptions<TextMarkContext> options)
            : base(options)
        {
        }

        
        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    base.OnModelCreating(modelbuilder);
        //    modelbuilder.Entity<Users_TB>().HasKey(x => new { x.Username, x.Project_ID });
        //}

        public DbSet<Roles_TB> Roles_TB { get; set; }
        public DbSet<Users_TB> Users_TB { get; set; }
        public DbSet<Labels_TB> Labels_TB { get; set; }
        public DbSet<Annotations_TB> Annotations_TB { get; set; } 
        public DbSet<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TB { get; set; }
        public DbSet<Projects_TB> Projects_TB { get; set; }       
    }    
}