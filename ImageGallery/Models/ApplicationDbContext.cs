using Image.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Imagedata> Images { get; set; }
    }
}

