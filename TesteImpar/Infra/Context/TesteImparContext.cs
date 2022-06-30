using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TesteImpar.Domain.Model;

#nullable disable

namespace TesteImpar.Context.Infra
{
    public partial class TesteImparContext : DbContext
    {
        public TesteImparContext()
        {
        }

        public TesteImparContext(DbContextOptions<TesteImparContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-7OM0BL6\\SQLEXPRESS;Initial Catalog=TesteImpar;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
                //optionsBuilder.UseSqlServer("Data Source=sql-marcel-nascimento.database.windows.net;Initial Catalog=SQLDB-MARCEL-NASCIMENTO;User ID=adm;Password=4qmXvSSH33aPtzmPKFVCWGjF9cdXQKcp; Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK__Car__PhotoID__267ABA7A");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.Base64).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
