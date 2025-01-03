using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities.Training;

namespace Workout.Infra.Data.Mappings
{
    public class TimelineMap : IEntityTypeConfiguration<Timeline>
    {
        public void Configure(EntityTypeBuilder<Timeline> builder)
        {
            builder.ToTable("Timeline");

            builder.Property(prop => prop.Id)
                   .HasColumnName("TimelineId");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.IsActive)
                   .HasConversion(new BoolToZeroOneConverter<int>())
                   .HasColumnName("Active")
                   .IsRequired();
        }
    }
}
