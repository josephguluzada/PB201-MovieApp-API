using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.DAL.Configurations;

namespace PB201MovieApp.DAL.Contexts;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}


    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
