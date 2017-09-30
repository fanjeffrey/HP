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

        public List<FieldValueType> ListFieldValueTypes()
        {
            return new List<FieldValueType> {
                FieldValueType.String,
                FieldValueType.Boolean,
                FieldValueType.Char,
                FieldValueType.Guid,
                FieldValueType.DateTime,
                FieldValueType.Int,
                FieldValueType.LongInt,
                FieldValueType.Float,
                FieldValueType.Double,
                FieldValueType.Decimal,
            };
        }

        public List<FieldValueChoiceMode> ListFieldValueChoiceModes()
        {
            return new List<FieldValueChoiceMode> {
                FieldValueChoiceMode.None,
                FieldValueChoiceMode.Single,
                FieldValueChoiceMode.Multi
            };
        }
    }
}
