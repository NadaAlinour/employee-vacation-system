using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class RuleService
    {
        private readonly IDataRepository2<Rule> _ruleRepository;
        private readonly IDataRepository<Unit> _unitRepository;

        public RuleService(IDataRepository2<Rule> ruleRepository, IDataRepository<Unit> unitRepository)
        {
            _ruleRepository = ruleRepository;
            _unitRepository = unitRepository;
        }

        public Rule GetRule(int id) {
            return _ruleRepository.Get(id);
        }
    }
}
