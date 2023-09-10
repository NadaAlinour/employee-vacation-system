using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class UnitService
    {
        private readonly IDataRepository<Unit> _unitRepository;

        public UnitService(IDataRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public IEnumerable<Unit> getAllUnits()
        {
            return _unitRepository.GetAll();
        }

        public Unit getUnit(int id)
        {
            return _unitRepository.Get(id);
        }
    }
}
