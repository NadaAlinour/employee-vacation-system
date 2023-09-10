using Microsoft.EntityFrameworkCore;
using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationPersistence.DatabaseAccess
{
    public class RuleManager : IDataRepository2<Rule>
    {

        readonly VacationDbContext _vacationDbContext;


        public RuleManager(VacationDbContext context)
        {
            _vacationDbContext = context;
        }

        public List<Rule> GetAll(int VID)
        {
            throw new NotImplementedException();
            //get all rules associated with given vacation id
            //return _vacationDbContext.Rules.Where(r => r.VacationID == VID).Include(u => u.Unit).ToList();
           
        }

        public void Add(Rule entity)
        {
            //throw new NotImplementedException();
            _vacationDbContext.Rules.Add(entity);
            _vacationDbContext.SaveChanges();
        }


        public void Update(Rule dbEntity, Rule entity)
        {
            //throw new NotImplementedException();

            dbEntity.NumberOfTimes = entity.NumberOfTimes;
            dbEntity.Duration = entity.Duration;
            dbEntity.UnitID = entity.UnitID;
            dbEntity.StartDate = entity.StartDate;
            dbEntity.EndDate = entity.EndDate;

            _vacationDbContext.SaveChanges();
        }
       


        public Rule Get(int id)
        {
            //throw new NotImplementedException();
            //return _vacationDbContext.Rules.Include(u => u.Unit).FirstOrDefault(r => r.VacationID == id);
            return _vacationDbContext.Rules.FirstOrDefault(r => r.VacationID == id); // get unitName in controller

        }







        public void Delete(Rule entity)
        {
            throw new NotImplementedException();
        }


    }
}
