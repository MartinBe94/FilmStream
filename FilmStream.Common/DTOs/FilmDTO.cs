namespace FilmStream.Common.DTOs;
public class FilmBaseDTO
{
   
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Released { get; set; }
    public bool Free { get; set; }
    public int DirectorId { get; set; }
    public string Description { get; set;}
    public string FilmUrl { get; set;}

}

public class FilmDTO:FilmBaseDTO
{
    public List<GenreBaseDTO> Genres { get; set; } = new();
    public List<SimilarFilmDTO> SimilarFilms { get; set; }
    public DirectorDTO Director { get; set; } = null!;


}

//public class FilmCreateDTO
//{
//    public string Title { get; set; }
//    public DateTime Released { get; set; }
//    public bool Free { get; set; }
//    public int DirectorId { get; set; }
//    public string Description { get; set; }
//    public string FilmUrl { get; set; }
//}

//public class FilmEditDTO : FilmCreateDTO
//{

//    public int Id { get; set; }
//}