namespace FilmStream.Common.Services
{
    public interface IMembershipService
    {
        Task<List<DirectorDTO>> GetDirectorAsync(bool freeOnly = false);
        Task<DirectorDTO> GetDirectorAsync(int id);
        Task<List<FilmBaseDTO>> GetFilmAsync(bool freeOnly = false);
        Task<FilmDTO> GetFilmAsync(int? id);
        Task<List<GenreBaseDTO>> GetGenreAsync(bool freeOnly = false);
        Task<GenreBaseDTO> GetGenreAsync(int id);
        Task<List<FilmGenreBaseDTO>> GetFilmGenreAsync();
        Task<FilmGenreBaseDTO> GetFilmGenreAsync(int filmid, int genreid);
    }
}