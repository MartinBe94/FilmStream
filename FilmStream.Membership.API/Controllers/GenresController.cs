using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmStream.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IDbService _db;

        public GenresController(IDbService db) => _db = db;

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                List<GenreBaseDTO>? genres = await _db.GetAsync<Genre, GenreBaseDTO>();

                return Results.Ok(genres);

            }
            catch { }
            return Results.NotFound();


        }


        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                var genre = await _db.SingleAsync<Genre, GenreBaseDTO>(d => d.Id.Equals(id));
                if (genre is null) return Results.NotFound();

                return Results.Ok(genre);
            }
            catch { }
            return Results.NotFound();
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreBaseDTO dto)
        {
            try
            {
                if (dto == null) { return Results.BadRequest(); }
                var genre = await _db.AddAsync<Genre, GenreBaseDTO>(dto);

                var success = await _db.SaveChangesAsync();

                if (!success) { return Results.BadRequest(); }

                return Results.Created(_db.GetURI<Genre>(genre), genre);

            }
            catch { }
            return Results.BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreBaseDTO dto)
        {

            try
            {
                if (dto == null) return Results.BadRequest("No entity provided");
                if (!id.Equals(dto.Id)) return Results.BadRequest("Id mismatch");

                var exists = await _db.AnyAsync<Genre>(c => c.Id.Equals(id));
                if (!exists) return Results.NotFound("Could not found");

                _db.Update<Genre, GenreBaseDTO>(dto.Id, dto);

                var success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
            }

            return Results.BadRequest("Unable to update the entity");
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var success = await _db.DeleteAsync<Genre>(id);

                if (!success) return Results.NotFound();

                success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();



                return Results.NoContent();

            }
            catch { }
            return Results.BadRequest("Unable to delete the entity");
        }
    }
}
