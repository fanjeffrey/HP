using HPCN.UnionOnline.Models;
using System.Collections.Generic;

namespace HPCN.UnionOnline.Services
{
    public interface IOptionsService
    {
        IList<Gender> ListGender();
        IList<EmployeeType> ListEmployeeTypes();
        List<PropertyValueType> ListPropertyValueTypes();
        List<PropertyValueChoiceMode> ListPropertyValueChoiceModes();
    }
}
