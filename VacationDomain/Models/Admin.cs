using System.ComponentModel.DataAnnotations;

namespace VacationDomain.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
