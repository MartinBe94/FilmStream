using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmStream.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService db) => _db = db;

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                List<FilmBaseDTO>? videos = await _db.GetAsync<Film, FilmBaseDTO>();

                return Results.Ok(videos);
            }
            catch
            {
            }

            return Results.NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                var video = await _db.SingleAsync<Film, FilmBaseDTO>(c => c.Id.Equals(id));
                if (video is null) return Results.NotFound();
                //var section = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(video.SectionId));
                //if (section is not null)
                //{
                //    video.SectionTitle = section.Title;
                //    var course = await _db.SingleAsync<Course, CourseDTO>(c => c.Id.Equals(section.CourseId));
                //    if (section is not null)
                //    {
                //        video.CourseTitle = course.Title;
                //        video.CourseId = course.Id;
                //    }
                //}

                return Results.Ok(video);
            }
            catch
            {
            }
            return Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmBaseDTO dto)
        {
            try
            {
                if (dto == null) return Results.BadRequest();

                var film = await _db.AddAsync<Film, FilmBaseDTO>(dto);

                var success = await _db.SaveChangesAsync();
                if (!success) return Results.BadRequest();

                return Results.Created(_db.GetURI<Film>(film), film);
            }
            catch
            {
            }

            return Results.BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmBaseDTO dto)
        {
            try
            {
                if (dto == null) return Results.BadRequest("No entity provided");
                if (!id.Equals(dto.Id)) return Results.BadRequest("Differing ids");

                var exists = await _db.AnyAsync<Director>(i => i.Id.Equals(dto.DirectorId));
                if (!exists) return Results.NotFound("Could not find related entity");

                exists = await _db.AnyAsync<Film>(c => c.Id.Equals(id));
                if (!exists) return Results.NotFound("Could not find entity");

                _db.Update<Film, FilmBaseDTO>(dto.Id, dto);

                var success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
            }

            return Results.BadRequest("Unable to update the entity");

        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var success = await _db.DeleteAsync<Film>(id);

                if (!success) return Results.NotFound();

                success = await _db.SaveChangesAsync();

                if (!success) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
            }

            return Results.BadRequest();
        }
    }
}
