using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using climby.Models;

namespace climby.Configurations
{
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
        public void Configure(EntityTypeBuilder<Shelter> builder)
        {
            builder.ToTable("CY_SHELTER");
            builder.HasKey(s => s.ID);

            builder.Property(s => s.ID).HasColumnName("ID");
            builder.Property(s => s.Name).HasColumnName("NAME");
            builder.Property(s => s.Email).HasColumnName("EMAIL");
            builder.Property(s => s.Phone).HasColumnName("PHONE");
            builder.Property(s => s.Country).HasColumnName("COUNTRY");
            builder.Property(s => s.City).HasColumnName("CITY");
            builder.Property(s => s.Adress).HasColumnName("ADRESS");
            builder.Property(s => s.AdressNumber).HasColumnName("ADRESS_NUMBER");
            builder.Property(s => s.District).HasColumnName("DISTRICT");
            builder.Property(s => s.IsFull).HasColumnName("IS_FULL");
            builder.Property(s => s.Cep).HasColumnName("CEP");
        }
    }
}
