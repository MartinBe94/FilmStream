var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy => {
policy.AddPolicy("CorsAllAccessPolicy", opt =>
opt.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());
});


ConfigureAutoMapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FilmStreamContext>(
options => options.UseSqlServer(
 builder.Configuration.GetConnectionString("StreamConnection")));
builder.Services.AddScoped<IDbService, DbService>();

void ConfigureAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
        cfg.CreateMap<Genre, GenreBaseDTO>().ReverseMap();
        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();
        cfg.CreateMap<Film, FilmBaseDTO>().ReverseMap();
        cfg.CreateMap<SimilarFilm, SimilarFilmBaseDTO>().ForMember(dest => dest.SimilarFilmId, src => src.MapFrom(s => s.SimilarFilmId)).ReverseMap();
        cfg.CreateMap<SimilarFilmBaseDTO, SimilarFilmDTO>().ReverseMap();
        cfg.CreateMap<FilmGenre, FilmGenreBaseDTO>().ForMember(dest => dest.FilmId, src => src.MapFrom(s => s.FilmId)).ReverseMap();
        cfg.CreateMap<FilmGenre, FilmGenreBaseDTO>().ReverseMap();
        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();
        cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
