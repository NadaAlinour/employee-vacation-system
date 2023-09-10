using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationPersistence.DatabaseAccess
{
    public class RequestManager : IDataRepository4<EmployeeVacation>
    {
        readonly VacationDbContext _vacationDbContext;

        public RequestManager(VacationDbContext context)
        {
            _vacationDbContext = context;
        }

        public void Add(EmployeeVacation entity)
        {
            //throw new NotImplementedException();
            _vacationDbContext.EmployeeVacations.Add(entity);
            _vacationDbContext.SaveChanges();
        }

        public List<EmployeeVacation> Get(int vid, int eid) 
        {
            //throw new NotImplementedException();
            return _vacationDbContext.EmployeeVacations.Where(x => x.VacationID == vid && x.EmployeeID == eid).ToList();

        }

        public List<EmployeeVacation> GetBasedOnHire(EmployeeVacation request)
        {
            //throw new NotImplementedException();
            int empId = request.EmployeeID;
            int vacId = request.VacationID;
            int startYear = request.StartDate.Year;

            return _vacationDbContext.EmployeeVacations.Where(x => x.EmployeeID == empId && x.VacationID == vacId && x.StartDate.Year == startYear).ToList();
        }

        public List<EmployeeVacation> GetPerMonth(EmployeeVacation request)
        {
            //throw new NotImplementedException();
            int empId = request.EmployeeID;
            int vacId = request.VacationID;
            int reqMonth = request.StartDate.Month;
            // add a condition to check for month in startdate
            return _vacationDbContext.EmployeeVacations.Where(x => x.EmployeeID == empId && x.VacationID == vacId && x.StartDate.Month == reqMonth).ToList();
        }

        public List<EmployeeVacation> GetPerPeriod(EmployeeVacation request)
        {
            //throw new NotImplementedException();
            int empId = request.EmployeeID;
            int vacId = request.VacationID;
            DateTime sDate = request.StartDate;
            DateTime eDate = request.ExpectedEndDate;

            return _vacationDbContext.EmployeeVacations.Where(x => x.EmployeeID == empId && x.VacationID == vacId && x.StartDate >= sDate && x.ExpectedEndDate <= eDate).ToList();
        
        
        }

        public List<EmployeeVacation> GetPerService(EmployeeVacation request)
        {
            //throw new NotImplementedException();
            int empId = request.EmployeeID;
            int vacId = request.VacationID;

            return _vacationDbContext.EmployeeVacations.Where(x => x.EmployeeID == empId && x.VacationID == vacId).ToList();
        }

        public List<EmployeeVacation> GetAll(int eid)
        {
            return _vacationDbContext.EmployeeVacations.Where(x => x.EmployeeID == eid).ToList();
        }
    }
}
