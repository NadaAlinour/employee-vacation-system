using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationApplication.Services;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class VacationService
    {
        private readonly IDataRepository<Vacation> _vacationRepository;
        private readonly IDataRepository5<Hire> _hireRepository;
        private readonly IDataRepository2<Rule> _ruleRepository;


        public VacationService(IDataRepository<Vacation> vacationRepository, IDataRepository5<Hire> hireRepository, IDataRepository2<Rule> ruleRepository)
        {
            _vacationRepository = vacationRepository;
            _hireRepository = hireRepository;
            _ruleRepository = ruleRepository;
        }

        public IEnumerable<Vacation> getAllVacations()
        {
            return _vacationRepository.GetAll();
        }

        public Vacation getVacation(int id)
        {
            return _vacationRepository.Get(id);
        }

        public Message addVacation(VacationInfo vacationInfo)
        {
            Message message = new Message();



            if (vacationInfo == null)
            {
                message.Type = "message";
                message.Content = "Vacation entered is null.";
                return message;
            }

            Vacation vacation = vacationInfo.vacation;
            List<Hire> hireList = vacationInfo.hires;
            Rule rule = vacationInfo.rule;


            if (vacation.IsBasedOnHire == true)
            {
                int vacation_id = _vacationRepository.Add(vacation);

                // add in hire table

                foreach (var h in hireList)
                    h.VacationID = vacation_id;

                _hireRepository.Add(hireList);


            }
            else
            {

                // if vacation is based on dates, check against duration
                var duration = rule.Duration;

                if (rule.StartDate != null && rule.EndDate != null)
                {

                    DateTime sDate = (DateTime)rule.StartDate;
                    DateTime eDate = (DateTime)rule.EndDate;
                    TimeSpan dateDuration = eDate - sDate;

                    if (duration != null)
                    {
                        if (dateDuration.Days < duration)
                        {
                            message.Type = "error";
                            message.Content = "Chosen duration exceeds period duration.";
                            return message;
                        }
                    }

                }

                int vacation_id = _vacationRepository.Add(vacation);

                // add in rule table
                rule.VacationID = vacation_id;
                _ruleRepository.Add(rule);
            }

            message.Type = "message";
            message.Content = "Vacation has been successfully added.";
            return message;
        }


        public Message updateVacation(int id, VacationInfo vacationInfo)
        {
            Message message = new Message();

            Vacation vacationToUpdate = _vacationRepository.Get(id);

            Vacation vacation = vacationInfo.vacation;
            List<Hire> hireList = vacationInfo.hires;
            Rule rule = vacationInfo.rule;


            if (vacationToUpdate.IsBasedOnHire == true)
            {

                _vacationRepository.Update(vacationToUpdate, vacation);


                List<Hire> hireListToUpdate = _hireRepository.GetAll(id);
                _hireRepository.Update(hireListToUpdate, hireList);

            }
            else
            {

                // if vacation is based on dates, check against duration
                if (rule.StartDate != null && rule.EndDate != null)
                {
                    var duration = rule.Duration;


                    DateTime sDate = (DateTime)rule.StartDate;
                    DateTime eDate = (DateTime)rule.EndDate;
                    TimeSpan dateDuration = eDate - sDate;

                    if (duration != null)
                    {
                        if (dateDuration.Days < duration)
                        {
                            message.Type = "error";
                            message.Content = "Chosen duration exceeds period duration.";
                            return message;
                        }
                    }

                }

                _vacationRepository.Update(vacationToUpdate, vacation);


                Rule ruleToUpdate = _ruleRepository.Get(id);
                _ruleRepository.Update(ruleToUpdate, rule);

            }

            message.Type = "message";
            message.Content = "Vacation has been successfully updated.";
            return message;

        }

        public Message deleteVacation(int id)
        {
            Message message = new Message();

            Vacation vacation = _vacationRepository.Get(id);

            if (vacation == null)
            {
                message.Type = "message";
                message.Content = "Vacation does not exist.";
                return message;
            }

            _vacationRepository.Delete(vacation);
            message.Type = "message";
            message.Content = "Vacation has been successfully deleted.";
            return message;
        }


    }
}
