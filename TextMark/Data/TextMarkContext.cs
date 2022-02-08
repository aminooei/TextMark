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

        
        public DbSet<Roles_TB> Roles_TB { get; set; }
        public DbSet<Users_TB> Users_TB { get; set; }
        public DbSet<Labels_TB> Labels_TB { get; set; }
        public DbSet<ClassificationLabels_TB> ClassificationLabels_TB { get; set; }
        public DbSet<Annotations_TB> Annotations_TB { get; set; } 
        public DbSet<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TB { get; set; }
        public DbSet<Assigned_TextClassifications_ToUsers_TB> Assigned_TextClassifications_ToUsers_TB { get; set; }        
        public DbSet<Projects_TB> Projects_TB { get; set; }
        public DbSet<ClassifiedTexts_Tags> ClassifiedTexts_Tags { get; set; }
        public DbSet<AnnotatedTexts_Tags> AnnotatedTexts_Tags { get; set; }
        public DbSet<Users_Access_Level_TB> Users_Access_Level_TB { get; set; }
    }    
}