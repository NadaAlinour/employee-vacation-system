namespace VacationApplication.Services
{
    public interface IDataRepository5<TEntity>
    {
        List<TEntity> GetAll(int VID);
        TEntity Get(int vid, int serviceYears);
        void Add(List<TEntity> entities);
        void Update(List<TEntity> dbEntities, List<TEntity> entities);
        void Delete(TEntity entity);
    }
}
