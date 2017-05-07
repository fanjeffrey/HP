using HPCN.UnionOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCN.UnionOnline.Services
{
    public interface IOptionsService
    {
        IList<Gender> ListGender();
        IList<EmployeeType> ListEmployeeTypes();
    }
}
