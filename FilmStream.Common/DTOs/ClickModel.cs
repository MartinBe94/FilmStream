namespace FilmStream.Common.DTOs;

public record ClickModel(string PageType,int Id);

public record ClickSimilarFilmModel(string PageType, int FilmId, int SimilarFilmId);
public record ClickFilmGenreModel(string PageType, int FilmId, int GenreId);