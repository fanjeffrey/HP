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

        public List<PropertyValueType> ListPropertyValueTypes()
        {
            return new List<PropertyValueType> {
                PropertyValueType.String,
                PropertyValueType.Boolean,
                PropertyValueType.Char,
                PropertyValueType.Guid,
                PropertyValueType.DateTime,
                PropertyValueType.Int,
                PropertyValueType.LongInt,
                PropertyValueType.Float,
                PropertyValueType.Double,
                PropertyValueType.Decimal,
            };
        }

        public List<PropertyValueChoiceMode> ListPropertyValueChoiceModes()
        {
            return new List<PropertyValueChoiceMode> {
                PropertyValueChoiceMode.None,
                PropertyValueChoiceMode.Single,
                PropertyValueChoiceMode.Multi
            };
        }
    }
}
