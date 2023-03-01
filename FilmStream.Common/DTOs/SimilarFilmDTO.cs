namespace FilmStream.Common.DTOs;
public class SimilarFilmBaseDTO
{
    public int FilmId { get; set; }
    public int SimilarFilmId { get; set; }
}
public class SimilarFilmDTO : SimilarFilmBaseDTO
{
    public FilmBaseDTO Film { get; set; } = new();
    public FilmBaseDTO Similar { get; set; } = null!;
}