using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;

namespace VacationApplication.DataService
{
    public class StatisticsService
    {
        private readonly IDataRepository3<VacationCount> _vacationCountRepository;

        public StatisticsService(IDataRepository3<VacationCount> vacationCountRepository)
        {
            _vacationCountRepository = vacationCountRepository;
        }

        public List<VacationCount> getAllStats()
        {
            return _vacationCountRepository.GetAll();
        }

        public List<VacationCount> getStats(DateObject dateObject)
        {
            return _vacationCountRepository.Get(dateObject.StartDate, dateObject.EndDate);
        }
    }
}
