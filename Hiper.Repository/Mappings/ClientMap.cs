using Hiper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiper.Repository.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Gender)
                .HasColumnName("Gender")
                .HasConversion<byte>()
                .HasColumnType("TINYINT");

            builder.Property(e => e.Birthday)
                .HasColumnName("Birthday")
                .HasColumnType("DATETIME");

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.FirstName)
          .IsRequired()
            .HasColumnName("FirstName")
              .HasColumnType("VARCHAR")
              .HasMaxLength(80);

                name.Property(n => n.LastName)
      .IsRequired()
        .HasColumnName("LastName")
          .HasColumnType("VARCHAR")
          .HasMaxLength(80);
            });

            builder.OwnsOne(x => x.Document, doc =>
            {
                doc.Property(d => d.Number)
          .IsRequired()
          .HasColumnName("Document")
          .HasColumnType("VARCHAR")
          .HasMaxLength(18);
            });

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.Street)
          .HasColumnName("Street")
   .HasColumnType("VARCHAR")
   .HasMaxLength(80);

                address.Property(a => a.Number)
          .HasColumnName("Number")
      .HasColumnType("VARCHAR")
      .HasMaxLength(20);

                address.Property(a => a.Neighborhood)
          .HasColumnName("Neighborhood")
      .HasColumnType("VARCHAR")
      .HasMaxLength(80);

                address.Property(a => a.City)
          .HasColumnName("City")
      .HasColumnType("VARCHAR")
      .HasMaxLength(80);

                address.Property(a => a.State)
          .HasColumnName("State")
      .HasColumnType("VARCHAR")
      .HasMaxLength(80);

                address.Property(a => a.Country)
          .HasColumnName("Country")
      .HasColumnType("VARCHAR")
      .HasMaxLength(80);

                address.Property(a => a.ZipCode)
          .HasColumnName("ZipCode")
      .HasColumnType("VARCHAR")
      .HasMaxLength(12);
            });


            builder.OwnsOne(c => c.Email, email =>
                {
                    email.Property(c => c.Address)
                  .IsRequired()
                  .HasColumnName("Email")
                  .HasColumnType("VARCHAR")
                  .HasMaxLength(150);
                });


        }
    }
}