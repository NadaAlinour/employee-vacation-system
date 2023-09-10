using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationPersistence.DatabaseAccess
{
    public class VacationManager : IDataRepository<Vacation>
    {
        readonly VacationDbContext _vacationDbContext;

        public VacationManager(VacationDbContext _context)
        {
                _vacationDbContext = _context;
        }

        public int Add(Vacation entity)
        {
            //throw new NotImplementedException();

            _vacationDbContext.Vacations.Add(entity);
            _vacationDbContext.SaveChanges();
            return entity.VacationID;
        }

        public void Delete(Vacation entity)
        {
            //throw new NotImplementedException();
            // since on delete it cascades i only need to delete the vacation and the corresponding rules etc will be deleted
            _vacationDbContext.Vacations.Remove(entity);
            _vacationDbContext.SaveChanges();
        }

        public Vacation Get(int id)
        {
            //throw new NotImplementedException();
            return _vacationDbContext.Vacations.FirstOrDefault(v => v.VacationID == id);
        }

        public IEnumerable<Vacation> GetAll()
        {
            //throw new NotImplementedException();
            return _vacationDbContext.Vacations.ToList();

        }

        public void Update(Vacation dbEntity, Vacation entity)
        {
            //throw new NotImplementedException();
            dbEntity.Name = entity.Name;
            dbEntity.Description = entity.Description;
            dbEntity.IsBasedOnHire = entity.IsBasedOnHire;
          
            _vacationDbContext.SaveChanges();
        }
    }
}
