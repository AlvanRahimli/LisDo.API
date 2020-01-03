using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Utilities
{
    public class ValidTypeAttribute : ValidationAttribute
    {
        private readonly string allowedType;

        public ValidTypeAttribute(string allowedType)
        {
            this.allowedType = allowedType;
        }

        public override bool IsValid(object value)
        {
            var allowedTypes = allowedType.ToUpper().Split(',');
            return allowedTypes.Contains(value.ToString().ToUpper());                
        }        
    }
}
