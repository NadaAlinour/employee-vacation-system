using VacationApplication.Repositories;
using VacationDomain.Models;
using VacationPersistence.Context;

namespace VacationPersistence.DatabaseAccess
{
    public class UserManager : IUserRepository<Admin>
    {
        readonly VacationDbContext _vacationDbContext;

        public UserManager(VacationDbContext vacationDbContext)
        {
            _vacationDbContext = vacationDbContext;
        }
        public Admin Get()
        {
            // retrieve the one admin in the database
            return _vacationDbContext.Admins.FirstOrDefault();
        }
    }
}
