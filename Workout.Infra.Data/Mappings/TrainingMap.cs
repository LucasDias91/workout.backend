using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities.Training;

namespace Workout.Infra.Data.Mappings
{
    public class TrainingMap : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("Training");

            builder.Property(prop => prop.Id)
                   .HasColumnName("TrainingId");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.IsActive)
                   .HasConversion(new BoolToZeroOneConverter<int>())
                   .HasColumnName("Active")
                   .IsRequired();
        }
    }
}
