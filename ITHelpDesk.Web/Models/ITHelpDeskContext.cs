using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITHelpDesk.Web.Models
{
    public partial class ITHelpDeskContext : DbContext
    {
        public ITHelpDeskContext()
        {
        }

        public ITHelpDeskContext(DbContextOptions<ITHelpDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:ithelpdedsk.database.windows.net,1433;Initial Catalog=ITHelpDesk;Persist Security Info=False;User ID=itadmin;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.AzureAdObjectId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired();

                entity.HasMany(e => e.TicketAssignedTos)
                    .WithOne(t => t.AssignedTo)
                    .HasForeignKey(t => t.AssignedToId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.TicketCreatedBies)
                    .WithOne(t => t.CreatedBy)
                    .HasForeignKey(t => t.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedDate).IsRequired();

                entity.HasOne(t => t.AssignedTo)
                    .WithMany(u => u.TicketAssignedTos)
                    .HasForeignKey(t => t.AssignedToId);

                entity.HasOne(t => t.CreatedBy)
                    .WithMany(u => u.TicketCreatedBies)
                    .HasForeignKey(t => t.CreatedById);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
