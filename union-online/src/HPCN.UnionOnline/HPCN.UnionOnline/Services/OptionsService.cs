using HPCN.UnionOnline.Models;
using System.Collections.Generic;

namespace HPCN.UnionOnline.Services
{
    public class OptionsService : IOptionsService
    {
        public IList<EmployeeType> ListEmployeeTypes()
        {
            return new List<EmployeeType> { EmployeeType.RE, EmployeeType.ETW };
        }

        public IList<Gender> ListGender()
        {
            return new List<Gender> { Gender.F, Gender.M };
        }
    }
}
