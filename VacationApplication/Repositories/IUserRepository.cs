using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationApplication.Repositories
{
    public interface IUserRepository<Admin>
    {
        public Admin Get(); // get the single admin in the table
    }
}
