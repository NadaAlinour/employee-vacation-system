using VacationDomain.Models;

namespace VacationApplication.Services
{
    public interface IDataRepository4<TEntity>
    {
        List<TEntity> Get(int vid, int eid); // get list of past requests with this vacation

        // get all past requests for an employee
        List<TEntity> GetAll(int eid);

        // get all requests for this employee based on number of times per service
        List<TEntity> GetPerService(EmployeeVacation request);

        // get all requests for this employee based on number of times per month
        List<TEntity> GetPerMonth(EmployeeVacation request);


        // get all requests for this employee within a specific period
        List<TEntity> GetPerPeriod(EmployeeVacation request);


        // get all requests for this employee based on hire date
        List<TEntity> GetBasedOnHire(EmployeeVacation request);


        void Add(TEntity entity); // add vacation request
       
    }
}
