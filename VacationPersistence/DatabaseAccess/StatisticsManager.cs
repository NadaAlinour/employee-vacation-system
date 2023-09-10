using VacationPersistence.Context;
using VacationApplication.Services;
using VacationApplication;
using VacationDomain.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace VacationPersistence.DatabaseAccess
{
    public class StatisticsManager : IDataRepository3<VacationCount>
    {

        readonly VacationDbContext _vacationDbContext;

        public StatisticsManager(VacationDbContext context)
        {
            _vacationDbContext = context;
        }


        // default - get alltime stats
        public List<VacationCount> GetAll()
        {
            var result = _vacationDbContext.Vacations   
                .GroupJoin(_vacationDbContext.EmployeeVacations,
                v => v.VacationID,
                e => e.VacationID,
                (v, e) => new VacationCount
                {
                    Name = v.Name,
                    NumberOfEmployees = e.Count()

                }).ToList();

            return result;

        }

        public List<VacationCount> Get(DateTime start, DateTime end)
        {
            // throw new NotImplementedException();

            var result = _vacationDbContext.Vacations
                .GroupJoin(_vacationDbContext.EmployeeVacations.Where(x => x.StartDate >= start
                  && x.ExpectedEndDate <= end),
                  v => v.VacationID,
                  e => e.VacationID,
                  (v, e) => new VacationCount
                  {
                      Name = v.Name,
                      NumberOfEmployees = e.Count()
                  }).ToList();
           
    

            return result;

        }
    }
}
