using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VacationDomain.Models
{
    public class EmployeeVacation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }
        public int EmployeeID { get; set; }
        //public Employee Employee { get; set; }
        public int VacationID { get; set; }
        //public Vacation Vacation { get; set; }

        //start date
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        //expected end date
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ExpectedEndDate { get; set; }

        //actual end date
        //[Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ActualEndDate { get; set; } // after employees return, not included in request

    }
}
