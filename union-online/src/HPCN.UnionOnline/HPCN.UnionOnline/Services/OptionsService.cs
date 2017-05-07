using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCN.UnionOnline.Models;

namespace HPCN.UnionOnline.Services
{
    public class OptionsService : IOptionsService
    {
        public IList<EmployeeType> ListEmployeeTypes()
        {
            return new List<EmployeeType> { EmployeeType.ETW, EmployeeType.RE };
        }

        public IList<Gender> ListGender()
        {
            return new List<Gender> { Gender.Female, Gender.Male };
        }
    }
}
