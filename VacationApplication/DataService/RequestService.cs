using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;
using Rule = VacationDomain.Models.Rule;

namespace VacationApplication.DataService
{
    public class RequestService
    {
        private readonly IDataRepository<Employee> _employeeRepository;
        private readonly IDataRepository4<EmployeeVacation> _employeeVacationRepository;
        private readonly IDataRepository<Vacation> _vacationRepository;
        private readonly IDataRepository5<Hire> _hireRepository;
        private readonly IDataRepository2<Rule> _ruleRepository;


        public RequestService(IDataRepository<Employee> employeeRepository, IDataRepository4<EmployeeVacation> employeeVacationRepository, IDataRepository<Vacation> vacationRepository, IDataRepository5<Hire> hireRepository, IDataRepository2<Rule> ruleRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeVacationRepository = employeeVacationRepository;
            _vacationRepository = vacationRepository;
            _hireRepository = hireRepository;
            _ruleRepository = ruleRepository;
        }

        // add request
        public Message AddRequest(EmployeeVacation employeeVacation)
        {
            // i want to return a success message or an error message
            Message message = new Message();


            int Eid = employeeVacation.EmployeeID;
            int Vid = employeeVacation.VacationID;

            // check if employee is allowed vacations
            Employee employee = _employeeRepository.Get(Eid);

            if (employee == null)
            {
                message.Type = "error";
                message.Content = "Employee does not exist.";
                return message;
            }

            if (!employee.IsVacationAllowed)
            {
                message.Type = "error";
                message.Content = "This employee is not allowed to register a vacation.";
                return message;
            }

            // check date validity
            DateTime sDate = employeeVacation.StartDate;
            DateTime eDate = employeeVacation.ExpectedEndDate;
            var requestedDuration = eDate - sDate;

            if (sDate > eDate) {
                message.Type = "error";
                message.Content = "Start date should be smaller than end date.";
                return message;
            }


            // check vacation overlap
            List<EmployeeVacation> allPastRequests = _employeeVacationRepository.GetAll(Eid);

            for (var i = 0; i < allPastRequests.Count; i++)
            {
                // reject if any vacation overlaps
                if (allPastRequests[i].StartDate >= sDate && allPastRequests[i].ExpectedEndDate <= eDate)
                {
                    message.Type = "error";
                    message.Content = "Vacation request clashes with a previously requested vacation.";
                    return message;
                }
            }


            // check vacation type
            Vacation vacation = _vacationRepository.Get(Vid);
            Rule rule = _ruleRepository.Get(Vid);


            if (vacation.IsBasedOnHire == false)
            {
                if (requestedDuration.TotalDays > rule.Duration)
                {
                    message.Type = "error";
                    message.Content = "The requested duration exceeds the vacation duration.";
                    return message;
                }

                if (rule.UnitID == null)
                {
                    // within specific period

                    // check that startdate/enddate of request lies within startdate/enddate of picked vacation

                    if (sDate < rule.StartDate || eDate > rule.EndDate)
                    {
                        message.Type = "error";
                        message.Content = "Requested start and end dates are not within the requested vacation's start and end dates.";
                        return message;
                    }   

                    List<EmployeeVacation> pastRequests = _employeeVacationRepository.GetPerPeriod(employeeVacation);
                    int numTimes = (int)rule.NumberOfTimes;

                    if (pastRequests.Count < numTimes)
                    {
                        _employeeVacationRepository.Add(employeeVacation);
                        message.Type = "success";
                        message.Content = "Request has been successfully added.";
                        return message;
                    }

                    else
                    {
                        message.Type = "error";
                        message.Content = "Request is denied. Number of times has already been reached for this employee within this period.";
                        return message;
                    }


                }

                else
                {
                    // if unit is not null, check if per month or per service and call appropriate methods

                    if (rule.UnitID == 2)
                    {
                        // per month

                        List<EmployeeVacation> pastRequests = _employeeVacationRepository.GetPerMonth(employeeVacation);

                        int numTimes = (int)rule.NumberOfTimes;

                        if (pastRequests.Count < numTimes)
                        {
                            _employeeVacationRepository.Add(employeeVacation);
                            message.Type = "success";
                            message.Content = "Request has been successfully added.";
                            return message;
                        }

                        else
                        {
                            message.Type = "error";
                            message.Content = "Request is denied. Number of times has already been reached for this employee this month.";
                            return message;
                        }
                    }

                    else if (rule.UnitID == 4)
                    {
                        // per service time
                        List<EmployeeVacation> pastRequests = _employeeVacationRepository.GetPerService(employeeVacation);
                        int numTimes = (int)rule.NumberOfTimes;

                        if (pastRequests.Count < numTimes)
                        {
                            _employeeVacationRepository.Add(employeeVacation);
                            message.Type = "success";
                            message.Content = "Request has been successfully added.";
                            return message;
                        }

                        else
                        {
                            message.Type = "error";
                            message.Content = "Request is denied. Number of times has already been reached for this vacation.";
                            return message;
                        }
                    }
                }

                //return Ok("placeholder");
                message.Type = "temp";
                message.Content = "";
            }

            else
            {
                // calculate number of years employee has worked according to hire date
                DateTime currentDate = DateTime.Now;
                int serviceYrs = (int)(currentDate - employee.HiredDate).TotalDays / 365;

                // get basedOnHire rule for this vacation where employee service time is between start and end years limit of rules
                Hire basedOnHireRule = _hireRepository.Get(Vid, serviceYrs);
                int numDays = basedOnHireRule.NumberOfDays; // total number of days allowed per year


                // get all past requests with empID and vacID in the specified start year
                List<EmployeeVacation> pastRequests = _employeeVacationRepository.GetBasedOnHire(employeeVacation);

                if (pastRequests.Count > 0)
                {

                    int tempDays = 0;
                    int numDaysRemaining = 0;
                    foreach (var req in pastRequests)
                    {
                        tempDays += (int)(req.ExpectedEndDate - req.StartDate).TotalDays;

                    }

                    numDaysRemaining = numDays - tempDays;

                    if (requestedDuration.TotalDays > numDaysRemaining)
                    {
                        message.Type = "error";
                        message.Content = "Number of days have already been reached for this employee this year";
                        return message;
                    }

                    // accept request
                    _employeeVacationRepository.Add(employeeVacation);
                    message.Type = "success";
                    message.Content = "Request has been successfully added.";
                    return message;
                }

                else
                {
                    if (requestedDuration.TotalDays > numDays)
                    {
                        message.Type = "error";
                        message.Content = "Requested duration exceeds the number of remaining days. Please choose different dates.";
                        return message;
                    }
                    else
                    {
                        // accept request
                        _employeeVacationRepository.Add(employeeVacation);
                        message.Type = "success";
                        message.Content = "Request has been successfully added.";
                        return message;
                    }
                }


            }


            return message;

        }
    }
}
