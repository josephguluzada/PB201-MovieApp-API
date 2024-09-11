using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.DAL.Configurations;

public class MovieImageConfiguration : IEntityTypeConfiguration<MovieImage>
{
    public void Configure(EntityTypeBuilder<MovieImage> builder)
    {
        builder.Property(x=>x.ImageUrl).IsRequired().HasMaxLength(100);
    }
}
