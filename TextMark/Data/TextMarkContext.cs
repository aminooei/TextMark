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

        public DbSet<Login> Logins { get; set; }
    }
}