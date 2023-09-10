using System.ComponentModel.DataAnnotations;

namespace VacationDomain.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }


        [Required]
        public string SkillName { get; set; }
    }
}
