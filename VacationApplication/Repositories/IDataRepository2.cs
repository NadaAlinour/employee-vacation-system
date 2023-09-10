namespace VacationApplication.Services
{
    public interface IDataRepository2<TEntity>
    {
        List<TEntity> GetAll(int VID);
        TEntity Get(int vid);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}
