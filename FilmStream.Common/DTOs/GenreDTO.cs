namespace FilmStream.Common.DTOs;

public class GenreBaseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class GenreDTO : GenreBaseDTO
{
    public List<FilmBaseDTO> Films { get; set; } = new();
}