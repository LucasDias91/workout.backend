using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities.Session;
using Workout.Domain.Emums;

namespace Workout.Infra.Data.Mappings
{
    public class SessionMap : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("SessionId");

            builder.Property(p => p.Key)
                   .HasColumnName("SessionKey");

            builder.Property(p => p.AuthType)
                   .HasConversion(new EnumToStringConverter<AuthTypes>());
 
            builder.Property(prop => prop.IsActive)
                   .HasColumnName("Active")
                   .HasConversion(new BoolToZeroOneConverter<int>());

        }
    }
}
