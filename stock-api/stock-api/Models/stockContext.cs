using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace stock_api.Models
{
    public partial class stockContext : DbContext
    {
        public stockContext()
        {
        }

        public stockContext(DbContextOptions<stockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<product> products { get; set; } = null!;
        public virtual DbSet<stock> stocks { get; set; } = null!;
        public virtual DbSet<transaction> transactions { get; set; } = null!;
        public virtual DbSet<transactionDetail> transactionDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=stock;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AI");

            modelBuilder.Entity<product>(entity =>
            {
                entity.HasKey(e => e.code);

                entity.ToTable("product");

                entity.Property(e => e.code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.name).HasMaxLength(500);
            });

            modelBuilder.Entity<stock>(entity =>
            {
                entity.HasKey(e => e.code);

                entity.ToTable("stock");

                entity.Property(e => e.code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.codeNavigation)
                    .WithOne(p => p.stock)
                    .HasForeignKey<stock>(d => d.code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stock_product");
            });

            modelBuilder.Entity<transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.transactionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<transactionDetail>(entity =>
            {
                entity.ToTable("transactionDetail");

                entity.Property(e => e.code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.codeNavigation)
                    .WithMany(p => p.transactionDetails)
                    .HasForeignKey(d => d.code)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transactionDetail_product");

                entity.HasOne(d => d.tran)
                    .WithMany(p => p.transactionDetails)
                    .HasForeignKey(d => d.tranId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transactionDetail_transaction");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
