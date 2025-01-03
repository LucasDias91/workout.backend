using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workout.Domain.Entities.User;

namespace Workout.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(prop => prop.Id)
                   .HasColumnName("UserId");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Key)
              .HasColumnName("UserKey");

            builder.Property(prop => prop.IsActive)
                   .HasConversion(new BoolToZeroOneConverter<int>())
                   .HasColumnName("Active")
                   .IsRequired();
        }
    }
}
