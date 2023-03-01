namespace FilmStream.Database.Entities;
public class SimilarFilm:IReferenceEntity
{
    // SimilarFilms is one to many and not many to many
    public int FilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public virtual Film Film { get; set; } = null!;
    [ForeignKey("SimilarFilmId")]
    public virtual Film Similar { get; set; } = null!;
}
