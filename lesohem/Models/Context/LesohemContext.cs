using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lesohem.Models.Context;

public partial class LesohemContext : DbContext
{
    public LesohemContext()
    {
    }

    public LesohemContext(DbContextOptions<LesohemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FormTraining> FormTrainings { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SocialNet> SocialNets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M508TRG;Database=lesohem;Trusted_Connection=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC071E64BE2D");

            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_City_To_Country");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC073ABAD677");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<FormTraining>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FormTrai__3214EC078C80FA52");

            entity.ToTable("FormTraining");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Group__3214EC075DEF0395");

            entity.ToTable("Group");

            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07C257C113");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SocialNet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SocialNe__3214EC07BB7BD2A7");

            entity.ToTable("SocialNet");

            entity.Property(e => e.Link).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC078EF80295");

            entity.ToTable("User");

            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.SurName).HasMaxLength(100);

            entity.HasOne(d => d.FormTraining).WithMany(p => p.Users)
                .HasForeignKey(d => d.FormTrainingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_To_FormTraining");

            entity.HasOne(d => d.Group).WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_To_Group");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_To_Role");

            entity.HasMany(d => d.Socials).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "SocialUser",
                    r => r.HasOne<SocialNet>().WithMany()
                        .HasForeignKey("SocialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SocialUser_SocialNet"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_SocialUser_User"),
                    j =>
                    {
                        j.HasKey("UserId", "SocialId");
                        j.ToTable("SocialUser");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
