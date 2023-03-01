namespace FilmStream.Membership.API.Extensions;

public static class HttpExtensions
{
    public static async Task<IResult> HttpGetAsync<TEntity, TDto>
            (this IDbService db)
            where TEntity : class, IEntity
            where TDto : class
    {

        var entities = await db.GetAsync<TEntity, TDto>();
        return Results.Ok(entities);
    }


    public static async Task<IResult> HttpGetAsync<TEntity, TDto>
        (this IDbService db, int id)
        where TEntity : class, IEntity
        where TDto : class
    {
        var entity = await db.SingleAsync<TEntity, TDto>(e =>
       e.Id.Equals(id));
        if (entity is null)
            return Results.NotFound();
        else
            return Results.Ok(entity);
    }



    public static async Task<IResult> HttpPostAsync<TEntity, TDto>
        (this IDbService db, TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {
        try
        {
            if (dto is null) return Results.BadRequest();

            var entity = await db.AddAsync<TEntity, TDto>(dto);

            if (await db.SaveChangesAsync())
            {
                return Results.Created(db.GetURI<TEntity>(entity), entity);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n.{ex}");
        }
        return Results.BadRequest();
    }
    public static async Task<IResult> HttpPutAsync<TEntity, TDto>
        (this IDbService db, TDto dto, int id)
        where TEntity : class, IEntity
        where TDto : class
    {
        try
        {
            if (!await db.AnyAsync<TEntity>(e => e.Id.Equals(id)))
            { return Results.NotFound(); }

            db.Update<TEntity, TDto>(id, dto);

            if (await db.SaveChangesAsync())
            { return Results.NoContent(); }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n.{ex}");
        }
        return Results.BadRequest();
    }
    public static async Task<IResult> HttpDeleteAsync<TEntity>
        (this IDbService db, int id)
   where TEntity : class, IEntity
    {
        try
        {
            var success = await db.DeleteAsync<TEntity>(id);
            if (await db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n.");
        }
        return Results.BadRequest();
    }

    public static async Task<IResult> HttpDeleteAsync<TReferenceEntity, TDto>(this IDbService db, TDto dto)
        where TReferenceEntity : class, IReferenceEntity where TDto : class
    {
        try
        {
            var success = db.Delete<TReferenceEntity, TDto>(dto);
            if (!db.Delete<TReferenceEntity, TDto>(dto)) return Results.NotFound();

            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest($"Couldn't delete the {typeof(TReferenceEntity).Name} entity.\n.");
        }

        return Results.BadRequest($"Couldn't delete the {typeof(TReferenceEntity).Name} entity.");
    }
    public static async Task<IResult> HttpAddAsync<TEntity, TDto>(this IDbService db, TDto dto)
        where TEntity : class, IReferenceEntity where TDto : class
    {
        try
        {
            var entity = await db.AddAsync<TEntity, TDto>(dto);
            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.");
    }
}