using JustWatch.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Infrastructure.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<TVShowActor> TVShowActors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<TVShowGenre> TVShowGenres { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<TVShowComment> TVShowComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MovieActor
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            // TVShowActor
            modelBuilder.Entity<TVShowActor>()
                .HasKey(sa => new { sa.TVShowId, sa.ActorId });

            modelBuilder.Entity<TVShowActor>()
                .HasOne(sa => sa.TVShow)
                .WithMany(s => s.TVShowActors)
                .HasForeignKey(sa => sa.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TVShowActor>()
                .HasOne(sa => sa.Actor)
                .WithMany(a => a.TVShowActors)
                .HasForeignKey(sa => sa.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            // MovieGenre 
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // TVShowGenre
            modelBuilder.Entity<TVShowGenre>()
                .HasKey(tg => new { tg.TVShowId, tg.GenreId });

            modelBuilder.Entity<TVShowGenre>()
                .HasOne(tg => tg.TVShow)
                .WithMany(s => s.TVShowGenres)
                .HasForeignKey(tg => tg.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TVShowGenre>()
                .HasOne(tg => tg.Genre)
                .WithMany(g => g.TVShowGenres)
                .HasForeignKey(tg => tg.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            // MovieComment
            modelBuilder.Entity<MovieComment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieComment>()
                .HasOne(c => c.User)
                .WithMany(u => u.MovieComments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieComment>()
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(2000);

            // TVShowComment
            modelBuilder.Entity<TVShowComment>()
                .HasOne(c => c.TVShow)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.TVShowId)
                .OnDelete (DeleteBehavior.Cascade);

            modelBuilder.Entity<TVShowComment>()
                .HasOne(c => c.User)
                .WithMany(u => u.TVShowComments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TVShowComment>()
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(2000);

            // Rating
            modelBuilder.Entity<Movie>()
                .Property(m => m.Rating)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TVShow>()
                .Property(x => x.Rating)
                .HasPrecision(3, 1);
        }

    }
}
