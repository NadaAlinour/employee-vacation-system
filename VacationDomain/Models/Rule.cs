using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;

namespace VacationDomain.Models

{
    public class Rule
    {

        // not everything is required here

        [Key]
        public int RuleID { get; set; }
    
        public Nullable<int> NumberOfTimes { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> UnitID { get; set; }
        //public Unit Unit { get; set; }
       /* public Nullable<int> HireID { get; set; }
        public Hire Hire { get; set; } */

        //start date
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> StartDate { get; set; }

        //end date
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> EndDate { get; set; }

        public int VacationID { get; set; }
        //public Vacation Vacation { get; set; } = null!;

        //public List<Unit> Units { get; set; } // not really a list it's just one value but idk 
        /*public List<Hire> Hires { get; set; } // also not sure*/
    }
}
