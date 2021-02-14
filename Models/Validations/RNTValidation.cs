using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models.Validations
{
    public class RNTValidation: AbstractValidator<RNT>
    {


        public RNTValidation()
        {
            RuleFor(rnt => rnt)
                .Must(rnt => IsValidTable(rnt))
                .WithMessage("The file data must be numbers");

            RuleFor(rnt => rnt)
                .Must(rnt => HasValidInterval(rnt))
                .WithMessage("The interval must be less than the numbers amount in table");
        }

        private bool HasValidInterval(RNT rnt)
        {
            int numbersAmount = 0;
            for (int i = 0; i < rnt.SourceTable.Rows.Count; i++)
            {
                numbersAmount += rnt.SourceTable.Rows[i].ItemArray.Count(item => item != null);
            }

            return numbersAmount > rnt.Interval;
            
        }

        private bool IsValidTable(RNT rnt)
        {
            bool isValid = true;

            for (int i = 0; i < rnt.SourceTable.Rows.Count; i++)
            {
                isValid = rnt.SourceTable.Rows[i].ItemArray.Any(data => !Regex.IsMatch(@"\d{1}", data.ToString()));
            }

            return isValid;

        }
    }
}
