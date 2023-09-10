using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Formats.Asn1;

namespace VacationDomain.Models
{
    public class Vacation
    {
        [Key]
        public int VacationID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Boolean IsBasedOnHire { get; set; }


    }
}
