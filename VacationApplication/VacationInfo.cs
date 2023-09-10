using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationDomain.Models;


namespace VacationApplication
{
        public class VacationInfo
    {
        public Vacation vacation { get; set; }
        public List<Hire> hires { get; set; }
        public Rule rule { get; set; }
    }
}
