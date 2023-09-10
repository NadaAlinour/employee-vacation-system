using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationDomain.Models;

namespace VacationApplication.Repositories
{
    public interface ISearchRepository<T1, T2>
    {
        public List<T1> getEmployees(int vacID);
        public List<T2> getVacations(int empID, DateTime fromDate, DateTime toDate);

    }
}
