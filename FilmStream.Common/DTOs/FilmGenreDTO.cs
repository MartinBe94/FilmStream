namespace FilmStream.Common.DTOs;


public class FilmGenreBaseDTO
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }
}
    public class FilmGenreDTO:FilmGenreBaseDTO
{
    public FilmBaseDTO Film { get; set; } = new();
    public GenreDTO Genre { get; set; } = new();
}
