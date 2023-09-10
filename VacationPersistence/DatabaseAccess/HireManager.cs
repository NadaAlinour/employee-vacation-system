using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationPersistence.DatabaseAccess
{
    public class HireManager : IDataRepository5<Hire>
    {
        readonly VacationDbContext _vacationDbContext;

        public HireManager(VacationDbContext _context)
        {
            _vacationDbContext = _context;
        }

        public void Add(List<Hire> entities)
        {
            //throw new NotImplementedException();

            foreach(var entity in entities)
                _vacationDbContext.Add(entity);

            _vacationDbContext.SaveChanges();
        }

        public void Delete(Hire entity)
        {
            throw new NotImplementedException();
        }

        public Hire Get(int vid, int serviceYears)
        {
            // throw new NotImplementedException();
            return _vacationDbContext.Hires.Where(x => x.StartYears <= serviceYears && x.EndYears >= serviceYears && x.VacationID == vid).FirstOrDefault();
        }


        public List<Hire> GetAll(int VID)
        {
            //throw new NotImplementedException();
           
            //get rules based on hired date for the specified vacation
            return _vacationDbContext.Hires.Where(h => h.VacationID == VID).ToList();

        }

        public void Update(List<Hire> dbEntities, List<Hire> entities)
        {
            //throw new NotImplementedException();

            //dbEntities.Clear();

            for(var i =  0; i < entities.Count; i++)
            {
                //dbEntities.Add(entities[i]);

                dbEntities[i].NumberOfDays = entities[i].NumberOfDays;
                dbEntities[i].StartYears = entities[i].StartYears;
                dbEntities[i].EndYears = entities[i].EndYears;

            }


            _vacationDbContext.SaveChanges();
        }
    }
}
