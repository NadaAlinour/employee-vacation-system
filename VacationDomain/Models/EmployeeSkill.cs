namespace VacationDomain.Models
{
    public class EmployeeSkill
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int SkillID { get; set; }    
        public Skill Skill { get; set; }
    }
}
