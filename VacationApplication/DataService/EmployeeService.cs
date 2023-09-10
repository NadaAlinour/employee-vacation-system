using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class EmployeeService
    {
        private readonly IDataRepository<Employee> _employeeRepository;

        public EmployeeService(IDataRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // get all employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        // get employee
        public Employee GetEmployee(int id)
        {
            return _employeeRepository.Get(id);
        }


        // add employee
        public void AddEmployee(Stream fileStream, string Name, string Phone, string Email, decimal Salary, DateTime HiredDate, Boolean IsVacationAllowed)
        {
            // throw new NotImplementedException();
            Employee employee = new Employee();
            employee.Name = Name;
            employee.Phone = Phone;
            employee.Email = Email;
            employee.Salary = Salary;
            employee.HiredDate = HiredDate;
            employee.IsVacationAllowed = IsVacationAllowed;

            if (fileStream.Length > 0)
            { 
                var memoryStream = new MemoryStream();

                fileStream.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                employee.Image = fileBytes;
            }

            _employeeRepository.Add(employee);
           
        }

        // update employee
        public int UpdateEmployee(int id, Stream fileStream, string Name, string Phone, string Email, decimal Salary, Boolean IsVacationAllowed)
        {
            //throw new NotImplementedException();

            // check that employee exists
            Employee employeeToUpdate = _employeeRepository.Get(id);

            if (employeeToUpdate != null)
            {

                Employee employee = new Employee();
                employee.Name = Name;
                employee.Phone = Phone;
                employee.Email = Email;
                employee.Salary = Salary;
                employee.IsVacationAllowed = IsVacationAllowed;

                if (fileStream.Length > 0)
                {
                    var memoryStream = new MemoryStream();

                    fileStream.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    employee.Image = fileBytes;
                }

                _employeeRepository.Update(employeeToUpdate, employee);
                return 1;

            }

            return -1;

        }

        // delete employee
        public int DeleteEmployee(int id)
        {
            // check that employee exists
            Employee employee = _employeeRepository.Get(id);

            if (employee != null)
            {
                _employeeRepository.Delete(employee);
                return 1; // employee has been successfully deleted

            }
            else
            {
                return -1; // employee does not exist
            }


        }


    }
}
