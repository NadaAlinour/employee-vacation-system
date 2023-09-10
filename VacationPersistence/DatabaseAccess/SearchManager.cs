using Microsoft.EntityFrameworkCore.Internal;
using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;
using VacationApplication.Repositories;

namespace VacationPersistence.DatabaseAccess
{
    public class SearchManager : ISearchRepository<Employee, Vacation>
    {
        readonly VacationDbContext _vacationDbContext;

        public SearchManager(VacationDbContext context)
        {
            _vacationDbContext = context;
        }

        public List<Employee> getEmployees(int vacID)
        {
            //throw new NotImplementedException();
            // join with employeeVacation table
            // join with employee table

            //don't retrieve those of deleted employees

            var result = _vacationDbContext.EmployeeVacations
           .Where(x => x.VacationID == vacID)
              .Join(_vacationDbContext.Employees.Where(d => d.IsDeleted == false),
              x => x.EmployeeID,
              e => e.EmployeeID,
              (x, e) => new Employee
              {
                  EmployeeID = e.EmployeeID,
                  Name = e.Name,
                  Image = e.Image,
                  HiredDate = e.HiredDate,
                  Phone = e.Phone,
                  Email = e.Email,
                  Salary = e.Salary,
                  IsVacationAllowed = e.IsVacationAllowed,
              }).ToList();

             return result;

        }

        public List<Vacation> getVacations(int empID, DateTime fromDate, DateTime toDate)
        {
            // throw new NotImplementedException();

            var result = _vacationDbContext.EmployeeVacations
                .Where(x => x.EmployeeID == empID && x.StartDate >= fromDate && x.ExpectedEndDate <= toDate)
                .Join(_vacationDbContext.Vacations,
                ev => ev.VacationID,
                e => e.VacationID,
                (ev, e) => new Vacation
                {
                    VacationID = e.VacationID,
                    Name = e.Name,
                    Description = e.Description,
                    IsBasedOnHire = e.IsBasedOnHire
                }).ToList();

            return result;
        }

    }
}
