using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationPersistence.DatabaseAccess
{
    public class UnitManager : IDataRepository<Unit> 
    {
        readonly VacationDbContext _vacationDbContext;

        public UnitManager(VacationDbContext _context)
        {
            _vacationDbContext = _context;
        }

        public int Add(Unit entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Unit entity)
        {
            throw new NotImplementedException();
        }

        public Unit Get(int id)
        {
            //throw new NotImplementedException();
            return _vacationDbContext.Units.FirstOrDefault(u => u.UnitID == id);
        }

        public IEnumerable<Unit> GetAll()
        {
            //throw new NotImplementedException();

            return _vacationDbContext.Units.ToList();

        }

        public void Update(Unit dbEntity, Unit entity)
        {
            throw new NotImplementedException();
        }
    }
}
