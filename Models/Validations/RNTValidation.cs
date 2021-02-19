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
                .WithMessage("A tabela deve ter apenas de números");

            RuleFor(rnt => rnt)
                .Must(rnt => HasValidInterval(rnt))
                .WithMessage("O intervalo deve ser menor que a quantidade de números na tabela");

            RuleFor(rnt => rnt)
                .Must(rnt => HasValidNumberLimit(rnt))
                .WithMessage("O intervalo deve ser menor que a quantidade de algarismos do valor limite");

            RuleFor(prop => prop)
                  .Must(rnt => !HasNegativeParameters(rnt))
                  .WithMessage("intervalo, valor limite e quantidade de números devem ser positivos");


        }



        private bool IsValidTable(RNT rnt)
        {


            for (int i = 0; i < rnt.SourceTable.Rows.Count; i++)
            {
                if (rnt.SourceTable.Rows[i].ItemArray.Any(data => Regex.IsMatch(@"[A-Za-z]", data.ToString()))) return false;
            }

            return true;

        }

        private bool HasValidNumberLimit(RNT rnt)
        {

            StringBuilder numberResult = new StringBuilder();

            numberResult.Append(1);
            for (int i = 1; i < rnt.Interval.ToString().Length; i++)
            {
                numberResult.Append(0);
            }


            if (rnt.ValueLimit < int.Parse(numberResult.ToString())) return false;

            return true;
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

        private bool HasNegativeParameters(RNT rnt)
        {
            return (rnt.Interval <= 0) && (rnt.ValueLimit <= 0) && (rnt.NumbersLimitAmount <= 0);
        }
    }
}
