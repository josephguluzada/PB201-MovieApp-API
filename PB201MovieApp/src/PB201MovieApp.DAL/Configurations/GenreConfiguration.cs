using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.DAL.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}
