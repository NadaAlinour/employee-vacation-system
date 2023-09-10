namespace VacationApplication.Services
{
    public interface IDataRepository3<TEntity>
    {
        List<TEntity> GetAll();

        List<TEntity> Get(DateTime start, DateTime end);

    }
}
