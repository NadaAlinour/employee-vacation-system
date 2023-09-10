using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Repositories;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class SearchService
    {
        private readonly ISearchRepository<Employee, Vacation> _searchRepository;
        private readonly IDataRepository<Employee> _employeeRepository;


        public SearchService(ISearchRepository<Employee, Vacation> searchRepository, IDataRepository<Employee> employeeRepository)
        {
            _searchRepository = searchRepository;
            _employeeRepository = employeeRepository;
        }

        public List<Employee> searchByVacation(int id) {
            return _searchRepository.getEmployees(id);
        }

        public List<Vacation> searchByEmployee(int id, DateObject dateObject)
        {
            
            return _searchRepository.getVacations(id, dateObject.StartDate, dateObject.EndDate);

        }


    }
}
