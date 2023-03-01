using FilmStream.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmStream.Database.Contexts;
public class FilmStreamContext: DbContext
{
    public virtual DbSet<Film> Films { get; set; } = null!;
    public virtual DbSet<SimilarFilm> SimilarFilms { get; set; } = null!;
    public virtual DbSet<Director> Directors { get; set; } = null!;
    public virtual DbSet<Genre> Genres { get; set; } = null!;
    public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;

    public FilmStreamContext(DbContextOptions<FilmStreamContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Composit Keys */
        modelBuilder.Entity<SimilarFilm>().HasKey(ci => new { ci.FilmId, ci.SimilarFilmId });
        modelBuilder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });

        /* Configuring related tables for the Film table*/
        modelBuilder.Entity<Film>(entity =>
        {
            entity
                // For each film in the Film Entity,
                // reference relatred films in the SimilarFilms entity
                // with the ICollection<SimilarFilms>
                .HasMany(d => d.SimilarFilms)
                .WithOne(p => p.Film)
                .HasForeignKey(d => d.FilmId)
                // To prevent cycles or multiple cascade paths.
                // Neded when seeding similar films data.
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure a many-to-many realtionship between genres
            // and films using the FilmGenre connection entity.
            entity.HasMany(d => d.Genres)
                  .WithMany(p => p.Films)
                  .UsingEntity<FilmGenre>()
                  // Specify the table name for the connection table
                  // to avoid duplicate tables (FilmGenre and FilmGenres)
                  // in the database.
                  .ToTable("FilmGenres");
        });

        /* Seed Data */
        modelBuilder.Entity<Director>().HasData(
            new { Id = 1, Name = "Tim Miller" },
            new { Id = 2, Name = "Jon Watts" },
            new { Id = 3, Name = "Christopher Nolan" },
            new { Id = 4, Name = "Brian De Palma" });

        modelBuilder.Entity<Entities.Film>().HasData(
            new
            {
                Id = 1,
                Title = "Deadpool",
                Released = new DateTime(2016, 02, 16),
                DirectorId = 1,
                Free = true,
                Description = "Deadpool Rated-R Marvel Movie",
                FilmUrl = "https://www.youtube.com/embed/ONHBaC-pfsk"
            },
                new
                {
                    Id = 2,
                    Title = "Spider-Man: No Way Home",
                    Released = new DateTime(2021, 12, 13),
                    DirectorId = 2,
                    Free = true,
                    Description = "Spiderman Movie",
                    FilmUrl = "https://www.youtube.com/embed/JfVOs4VSpmA"
                },
                    new
                    {
                        Id = 3,
                        Title = "Batman The Dark Knight",
                        Released = new DateTime(2008, 07, 14),
                        DirectorId = 3,
                        Free = true,
                        Description = "The best Batman movie",
                        FilmUrl = "https://www.youtube.com/embed/EXeTwQWrcwY"
                    }, new
                    {
                        Id = 4,
                        Title = "Scarface",
                        Released = new DateTime(1983, 12, 1),
                        DirectorId = 4,
                        Free = true,
                        Description = "The Classical Scarface moive.",
                        FilmUrl = "https://www.youtube.com/embed/cv276Wg3e7I"
                    });

        modelBuilder.Entity<SimilarFilm>().HasData(
            new SimilarFilm { FilmId = 1, SimilarFilmId = 2 },
            new SimilarFilm { FilmId = 1, SimilarFilmId = 3 },
            new SimilarFilm { FilmId = 2, SimilarFilmId = 1 },
            new SimilarFilm { FilmId = 2, SimilarFilmId = 3 },
            new SimilarFilm { FilmId = 3, SimilarFilmId = 1 },
            new SimilarFilm { FilmId = 3, SimilarFilmId = 2 });

        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Adventure" },
            new { Id = 3, Name = "Sci-Fi" },
            new { Id = 4, Name = "Horror" },
            new { Id = 5, Name = "Drama" },
            new { Id = 6, Name = "Criminal" });

        modelBuilder.Entity<FilmGenre>().HasData(
        new FilmGenre { FilmId = 1, GenreId = 1 },
        new FilmGenre { FilmId = 1, GenreId = 2 },
        new FilmGenre { FilmId = 2, GenreId = 1 },
        new FilmGenre { FilmId = 2, GenreId = 2 },
        new FilmGenre { FilmId = 3, GenreId = 1 },
        new FilmGenre { FilmId = 3, GenreId = 2 },
        new FilmGenre { FilmId = 4, GenreId = 5 },
        new FilmGenre { FilmId = 4, GenreId = 6 });
    }
}
