using Microsoft.EntityFrameworkCore;
//using VacationPersistence.Database.Models;
using VacationDomain.Models;
using System.Drawing;

namespace VacationPersistence.Context
{
    public class VacationDbContext : DbContext
    {
        public VacationDbContext(DbContextOptions options) : base(options)
        {
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using(var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Hire> Hires { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(emp => new { emp.EmployeeID, emp.SkillID });
 
        }

    }
}
