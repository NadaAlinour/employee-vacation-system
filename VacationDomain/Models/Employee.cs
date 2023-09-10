using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationDomain.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }


        // image - or image path?
        [Required]
        public byte[] Image { get; set; }


        // hired-at
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime HiredDate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public Boolean IsVacationAllowed { get; set; }

        [DefaultValue("false")]
        public Boolean IsDeleted { get; set; }

}

}
