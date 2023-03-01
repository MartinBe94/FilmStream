namespace FilmStream.Common.Services;

public class MembershipService : IMembershipService
{
    private readonly MembershipHttpClient _http;

    public MembershipService(MembershipHttpClient http)
    {
        _http = http;
    }

    public async Task<List<FilmBaseDTO>> GetFilmAsync(bool freeOnly = false)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmBaseDTO>>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }

    public async Task<FilmDTO> GetFilmAsync(int? id)//, bool freeOnly)
    {
        try
        {

            if (id is null) throw new ArgumentException("id");

            using HttpResponseMessage response = await _http.Client.GetAsync($"films/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmDTO>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }


    public async Task<List<FilmGenreBaseDTO>> GetFilmGenreAsync()
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync("filmgenres");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmGenreBaseDTO>>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }

    public async Task<FilmGenreBaseDTO> GetFilmGenreAsync(int filmid, int genreid)//, bool freeOnly)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"film/{filmid}genre/{genreid}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmGenreBaseDTO>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }

    public async Task<List<DirectorDTO>> GetDirectorAsync(bool freeOnly = false)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"directors?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<DirectorDTO>>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }


    public async Task<DirectorDTO> GetDirectorAsync(int id)//, bool freeOnly)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"directors/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<DirectorDTO>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }


    public async Task<List<GenreBaseDTO>> GetGenreAsync(bool freeOnly = false)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"genres?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<GenreBaseDTO>>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }

    public async Task<GenreBaseDTO> GetGenreAsync(int id)//, bool freeOnly)
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"genres/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<GenreBaseDTO>(await
                response.Content.ReadAsStreamAsync(), new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }
}
