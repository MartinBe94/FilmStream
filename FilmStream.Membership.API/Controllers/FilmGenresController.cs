using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmStream.Membership.API.Controllers
{

    [ApiController]
    public class FilmGenresController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmGenresController(IDbService db) => _db = db;

        [Route("api/FilmGenres")]
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<FilmGenre>();
                List<FilmGenreDTO>? filmgenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();
                

                return Results.Ok(filmgenres);

            }
            catch { }
            return Results.NotFound();


        }

        [Route("api/film/{filmId}/genre/{genreId}")]
        [HttpGet]
        public async Task<IResult> Get(int filmId, int genreId)
        {
            try
            {
        
                _db.Include<FilmGenre>();
                FilmGenreDTO? filmgenres = await _db.SingleAsync<FilmGenre, FilmGenreDTO>(
                    c => c.FilmId.Equals(filmId) && c.GenreId.Equals(genreId));

                return Results.Ok(filmgenres);
            }
            catch
            {
            }

            return Results.NotFound();
        }


        [Route("api/FilmGenres")]
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmGenreBaseDTO dto)
        {
            {
                try
                {
                    var entity = await _db.AddAsync<FilmGenre, FilmGenreBaseDTO>(dto);
                    if (await _db.SaveChangesAsync()) return Results.NoContent();
                }
                catch (Exception ex)
                {

                }

                return Results.BadRequest();
            }
        }

        [Route("api/film/{filmId}/genre/{genreId}")]
        [HttpDelete]
        public async Task<IResult> Delete(int filmId, int genreId)
        {
            try
            {
                var success =await _db.DeleteFilmGenreAsync(filmId, genreId);

                if (!await _db.SaveChangesAsync()) return Results.NotFound();

                return Results.NoContent();
            }
            catch
            {
            }

            return Results.BadRequest();
        }
    }
}
