using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationDomain.Models
{
    public class Hire
    {
        [Key]
        public int HireID { get; set; }

        [Required]
        public int NumberOfDays { get; set; }

        // start number of years
        [Required]
        public int StartYears { get; set; }

        // end number of years
        [Required]
        public int EndYears { get; set; }

        public int VacationID { get; set; } // set as foreign key in migration
    }
}
