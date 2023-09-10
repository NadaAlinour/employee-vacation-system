using Microsoft.AspNetCore.Http.HttpResults;
using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;


namespace VacationPersistence.DatabaseAccess

{
    public class EmployeeRepository : IDataRepository<Employee>
    {

        readonly VacationDbContext _vacationDbContext;


        public EmployeeRepository(VacationDbContext context)
        {
                _vacationDbContext = context;
        }

     
        public int Add(Employee entity)
        {
            _vacationDbContext.Employees.Add(entity);
            _vacationDbContext.SaveChanges();
            return entity.EmployeeID;
        }

        public void Delete(Employee dbEntity)
        {
             _vacationDbContext.Employees.Remove(dbEntity); 
             _vacationDbContext.SaveChanges(); 

            // set IsDeleted property
            
            /*dbEntity.IsDeleted = true;
            _vacationDbContext.SaveChanges();*/

        }

        public Employee Get(int id)
        {

            // return _vacationDbContext.Employees.Where(e => e.IsDeleted != true).FirstOrDefault(e => e.EmployeeID == id);
             return _vacationDbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

        }

        public IEnumerable<Employee> GetAll()
        {
            // return _vacationDbContext.Employees.Where(e => e.IsDeleted != true).ToList();
            return _vacationDbContext.Employees.ToList();

        }

        public void Update(Employee dbEntity, Employee entity)
        {
            //throw new NotImplementedException();
            dbEntity.Name = entity.Name;
            dbEntity.Image = entity.Image;
           // dbEntity.HiredDate = entity.HiredDate;
            dbEntity.Phone = entity.Phone;
            dbEntity.Email = entity.Email;
            dbEntity.Salary = entity.Salary;
            dbEntity.IsVacationAllowed = entity.IsVacationAllowed;
            _vacationDbContext.SaveChanges();

        }
    }
}
