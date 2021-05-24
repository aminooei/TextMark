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

        public DbSet<Users_TB> Users_TBs { get; set; }
        public DbSet<Labels_TB> Labels_TBs { get; set; }
        public DbSet<Annotations_Labels_TB> Annotations_Labels_TBs { get; set; }
        public DbSet<Annotations_TB> Annotation_TBs { get; set; }        
        public DbSet<Roles_TB> Roles_TBs { get; set; }
        public DbSet<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TBs { get; set; }
    }
}