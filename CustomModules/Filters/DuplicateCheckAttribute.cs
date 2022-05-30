using CustomModules.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomModules.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DuplicateCheckAttribute: ValidationAttribute
    {
        public override bool IsValid(object objectValue)
        {
            bool result = true;
            
            if (objectValue != null)
            {
                SubmittedModel model = (SubmittedModel)objectValue;
                foreach (var item in model.DataItems)
                {
                    if (item.EmpId.Equals(model.NewData.EmpId) || item.Email.Equals(model.NewData.Email))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
