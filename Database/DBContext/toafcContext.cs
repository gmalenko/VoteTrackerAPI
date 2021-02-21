using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VoteTrackerAPI.Models.Database;

#nullable disable

namespace VoteTrackerAPI.Database.DBContext
{
    public partial class toafcContext : DbContext
    {
        public toafcContext()
        {
        }

        public toafcContext(DbContextOptions<toafcContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userpassword> Userpasswords { get; set; }
        public virtual DbSet<VoteCandidate> VoteCandidates { get; set; }
        public virtual DbSet<VotePeriod> VotePeriods { get; set; }
        public virtual DbSet<VoteRegistration> VoteRegistrations { get; set; }
        public virtual DbSet<VoteSelfRegistration> VoteSelfRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=192.168.10.76;Database=toafc;Username=toafc;Password=toafc198d;Port=5432");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.IsDisabled).HasColumnName("is_disabled");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<Userpassword>(entity =>
            {
                entity.ToTable("userpassword");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VoteCandidate>(entity =>
            {
                entity.ToTable("vote_candidates");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Createdbyuser).HasColumnName("createdbyuser");

                entity.Property(e => e.Createdon).HasColumnName("createdon");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Modifiedbyuser).HasColumnName("modifiedbyuser");

                entity.Property(e => e.Modifiedon).HasColumnName("modifiedon");

                entity.Property(e => e.VotePeriod).HasColumnName("vote_period");

                entity.HasOne(d => d.VotePeriodNavigation)
                    .WithMany(p => p.VoteCandidates)
                    .HasForeignKey(d => d.VotePeriod)
                    .HasConstraintName("votecandidates_voteperiod");
            });

            modelBuilder.Entity<VotePeriod>(entity =>
            {
                entity.ToTable("vote_period");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<VoteRegistration>(entity =>
            {
                entity.ToTable("vote_registration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Createdbyuser).HasColumnName("createdbyuser");

                entity.Property(e => e.Createdon).HasColumnName("createdon");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.IsElgible).HasColumnName("is_elgible");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Modifiedbyuser).HasColumnName("modifiedbyuser");

                entity.Property(e => e.Modifiedon).HasColumnName("modifiedon");

                entity.Property(e => e.VotePeriod).HasColumnName("vote_period");

                entity.HasOne(d => d.VotePeriodNavigation)
                    .WithMany(p => p.VoteRegistrations)
                    .HasForeignKey(d => d.VotePeriod)
                    .HasConstraintName("voteregistration_voteperiod");
            });

            modelBuilder.Entity<VoteSelfRegistration>(entity =>
            {
                entity.ToTable("vote_self_registration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Createdbyuser).HasColumnName("createdbyuser");

                entity.Property(e => e.Createdon).HasColumnName("createdon");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Modifiedbyuser).HasColumnName("modifiedbyuser");

                entity.Property(e => e.Modifiedon).HasColumnName("modifiedon");

                entity.Property(e => e.VotePeriod).HasColumnName("vote_period");

                entity.HasOne(d => d.VotePeriodNavigation)
                    .WithMany(p => p.VoteSelfRegistrations)
                    .HasForeignKey(d => d.VotePeriod)
                    .HasConstraintName("voteselfregistration_voteperiod");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
