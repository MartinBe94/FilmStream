using FilmStream.Data.Interfaces;
using FilmStream.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Collections.Specialized.BitVector32;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmStream.Membership.API.Controllers
{
    [ApiController]
    public class SimilarFilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public SimilarFilmsController(IDbService db) => _db = db;

      
        [Route("api/SimilarFilms")]
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<SimilarFilm>();
                List<SimilarFilmDTO>? similarfilms = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>();
                

                return Results.Ok(similarfilms);

            }
            catch { }
            return Results.NotFound();


        }

        [Route("api/film/{filmId}/similarfilm/{similarId}")]
        [HttpGet]
        public async Task<IResult> Get(int filmId, int similarId)
        {
            try
            {
                _db.Include<SimilarFilm>();
                SimilarFilmDTO? similarfilm = await _db.SingleAsync<SimilarFilm, SimilarFilmDTO>(
                    c => c.FilmId.Equals(filmId) && c.SimilarFilmId.Equals(similarId));

                return Results.Ok(similarfilm);
            }
            catch
            {
            }

            return Results.NotFound();
        }


        [Route("api/SimilarFilms")]
        [HttpPost]
        public async Task<IResult> Post([FromBody] SimilarFilmBaseDTO dto)
        {
            {
                try
                {
                    var entity = await _db.AddAsync<SimilarFilm, SimilarFilmBaseDTO>(dto);
                    if (await _db.SaveChangesAsync()) return Results.NoContent();
                }
                catch (Exception ex)
                {

                }

                return Results.BadRequest();
            }
        }


        [Route("api/film/{filmId}/similarfilm/{similarId}")]
        [HttpDelete]
        public async Task<IResult> Delete(int filmId, int similarId)
        {
            try
            {
                var success = await _db.DeleteSimilarFilmAsync(filmId, similarId);
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
