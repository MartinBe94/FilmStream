﻿namespace FilmStream.Database.Interfaces
{
    public interface IDbService
    {
        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
            where TEntity : class
            where TDto : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity;

        bool Delete<TReferenceEntity, TDto>(TDto dto)
            where TReferenceEntity : class
            where TDto : class;

        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;

        Task<bool> DeleteSimilarFilmAsync(int filmId, int similarFilmId);
        Task<bool> DeleteFilmGenreAsync(int filmId, int genreId);

        Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class
        where TDto : class;
        Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
            where TDto : class;
        //Task<List<TDto>> GetAsync<TEntity, TDto>()
        //    where TEntity : class, IEntity
        //    where TDto : class;
        //Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        //    where TEntity : class, IEntity
        //    where TDto : class;
        string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Include<TEntity>() where TEntity : class;
        Task<bool> SaveChangesAsync();
        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
            where TEntity : class
            where TDto : class;
        void Update<TEntity, TDto>(int id, TDto dto)
            where TEntity : class, IEntity
            where TDto : class;
    }
}