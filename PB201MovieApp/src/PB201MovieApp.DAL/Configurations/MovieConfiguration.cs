using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.DAL.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x=>x.Desc).IsRequired(false).HasMaxLength(800);

        builder.HasOne(x=>x.Genre)
            .WithMany(x=>x.Movies)
            .HasForeignKey(x=>x.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
