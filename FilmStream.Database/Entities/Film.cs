namespace FilmStream.Database.Entities;
public class Film:IEntity
{
    public Film()
    {
        SimilarFilms = new HashSet<SimilarFilm>();
        Genres = new HashSet<Genre>();
    }
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Released { get; set; }
    public bool Free { get; set; }
    public int DirectorId { get; set; }
    public string Description { get; set;}
    public string FilmUrl { get; set;}
    public virtual ICollection<SimilarFilm> SimilarFilms { get; set; }
    public virtual ICollection<Genre> Genres { get; set; }
    public virtual Director Director { get; set; } = null!;
}
