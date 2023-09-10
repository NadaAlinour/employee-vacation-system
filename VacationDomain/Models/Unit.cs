using System.ComponentModel.DataAnnotations;

namespace VacationDomain.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        [Required]
        public string UnitName { get; set; }

       
    }
}
