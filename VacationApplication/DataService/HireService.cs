using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class HireService
    {
        private readonly IDataRepository5<Hire> _hireRepository;

        public HireService(IDataRepository5<Hire> hireRepository)
        {
            _hireRepository = hireRepository;
        }
        public List<Hire> getHireRules(int vid)
        {
            return _hireRepository.GetAll(vid);
        }
    }
}
