using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using climby.Models;

namespace climby.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.ToTable("CY_USER");
            builder.HasKey(u => u.ID);

            builder.Property(u => u.ID).HasColumnName("ID");
            builder.Property(u => u.Firebase_uid).HasColumnName("FIREBASE_UID");
            builder.Property(u => u.Name).HasColumnName("NAME");
            builder.Property(u => u.Email).HasColumnName("EMAIL");
            builder.Property(u => u.Country).HasColumnName("COUNTRY");
            builder.Property(u => u.City).HasColumnName("CITY");
        }
    }
}
