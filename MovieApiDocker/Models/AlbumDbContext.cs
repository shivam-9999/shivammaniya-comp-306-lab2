using System;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Models
{

    public class AlbumDbContext : DbContext
    {
        public AlbumDbContext(DbContextOptions<AlbumDbContext> options) : base(options)
        {
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
