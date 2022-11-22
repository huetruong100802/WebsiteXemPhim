using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<People> Peoples { get; set; }
    public virtual DbSet<MoviePeople> MoviePeoples { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<MovieStatus> MovieStatuses { get; set; }
    public virtual DbSet<FollowedMovie> FollowedMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database= MovieDb;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.MovieId, "IX_Comments_MovieId");

            entity.HasIndex(e => e.UserId, "IX_Comments_UserId");

            entity.Property(e => e.MovieId).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.Movie).WithMany(p => p.Comments).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.ToTable("Episode");

            entity.HasIndex(e => e.MovieId, "IX_Episode_MovieId");

            entity.Property(e => e.Title).HasMaxLength(60);
            entity.Property(e => e.VideoName).HasMaxLength(100);

            entity.HasOne(d => d.Movie).WithMany(p => p.Episodes).HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movie");

            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(60);
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.ToTable("MovieGenre");

            entity.HasIndex(e => e.GenreId, "IX_MovieGenre_GenreId");

            entity.HasIndex(e => e.MovieId, "IX_MovieGenre_MovieId");

            entity.HasOne(d => d.Genre).WithMany(p => p.MovieGenres).HasForeignKey(d => d.GenreId);

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieGenres).HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasIndex(e => e.MovieId, "IX_Rates_MovieId");

            entity.HasIndex(e => e.UserId, "IX_Rates_UserId");

            entity.HasOne(d => d.Movie).WithMany(p => p.Rates).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.User).WithMany(p => p.Rates).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<People>(entity =>
        {
            entity.ToTable("People");
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });
        modelBuilder.Entity<MoviePeople>(entity =>
        {
            entity.ToTable("MoviePeople");
            entity.HasIndex(e => e.MovieId, "IX_MoviePeople_MovieId");

            entity.HasIndex(e => e.PeopleId, "IX_MoviePeople_PeopleId");
            entity.HasIndex(e => e.RoleId, "IX_MoviePeople_RoleId");

            entity.HasOne(d => d.Movie).WithMany(p => p.MoviePeople).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.People).WithMany(p => p.MoviePeoples).HasForeignKey(d => d.PeopleId);
            entity.HasOne(d => d.Roles).WithMany(p => p.MoviePeoples).HasForeignKey(d => d.RoleId);
        });
        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");
        });
        modelBuilder.Entity<MovieStatus>(entity =>
        {
            entity.ToTable("MovieStatus");
            entity.HasIndex(e => e.MovieId, "IX_MovieStatus_MovieId");
            entity.HasIndex(e => e.StatusId, "IX_MovieStatus_StatusId");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieStatuses).HasForeignKey(d => d.MovieId);
            entity.HasOne(d => d.Status).WithMany(p => p.MovieStatuses).HasForeignKey(d => d.StatusId);
        });
        modelBuilder.Entity<FollowedMovie>(entity =>
        {
            entity.ToTable("FollowedMovie");
            entity.HasIndex(e => e.MovieId, "IX_FollowedMovies_MovieId");

            entity.HasIndex(e => e.UserId, "IX_FollowedMovies_UserId");

            entity.HasOne(d => d.Movie).WithMany(p => p.FollowedMovies).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.User).WithMany(p => p.FollowedMovies).HasForeignKey(d => d.UserId);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
