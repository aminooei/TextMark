using Microsoft.EntityFrameworkCore;
using TextMark.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace TextMark.Data
{
    public class TextMarkContext : IdentityDbContext
    {
        public TextMarkContext(DbContextOptions<TextMarkContext> options)
            : base(options)
        {
        }

        public DbSet<Roles_TB> Roles_TB { get; set; }
        public DbSet<Users_TB> Users_TB { get; set; }
        public DbSet<Labels_TB> Labels_TB { get; set; }
        public DbSet<Annotations_Labels_TB> Annotations_Labels_TB { get; set; }
        public DbSet<Annotations_TB> Annotations_TB { get; set; } 
        public DbSet<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TB { get; set; }
    }
}