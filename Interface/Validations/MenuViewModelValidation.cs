using FluentValidation;
using Interface.ViewModels;
using Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interface.Validations
{
    public class MenuViewModelValidation : AbstractValidator<MenuViewModel>
    {
        public MenuViewModelValidation()
        {
            //Null check
            RuleFor(prop => prop.Interval)
                 .Must(prop =>!string.IsNullOrWhiteSpace(prop))
                 .WithMessage("O intervalo não pode ser vazio");

            RuleFor(prop => prop.NumbersAmount)
               .Must(prop => !string.IsNullOrWhiteSpace(prop))
               .WithMessage("A quantidade de números não pode ser vazia");

            RuleFor(prop => prop.ValueLimit)
               .Must(prop => !string.IsNullOrWhiteSpace(prop))
               .WithMessage("O valor limite não pode ser vazio");


            //Number Check
            RuleFor(prop => prop.Interval)
                .Must(prop => Regex.IsMatch(prop, @"^\d{1,}$"))
                .When(prop => !string.IsNullOrWhiteSpace(prop.Interval))
                .WithMessage("O intervalo deve ser um número");

            RuleFor(prop => prop.NumbersAmount)
                .Must(prop => Regex.IsMatch(prop, @"^\d{1,}$"))
               .When(prop => !string.IsNullOrWhiteSpace(prop.NumbersAmount))
                .WithMessage("A quantidade de números deve ser um campo numérico");

            RuleFor(prop => prop.ValueLimit)
              .Must(prop=> Regex.IsMatch(prop, @"^\d{1,}$"))
              .When(prop => !string.IsNullOrWhiteSpace(prop.ValueLimit))
                .WithMessage("O valor limite deve ser um número");

            RuleFor(prop => prop.StartColumnIndex)
                .Must(prop => Regex.IsMatch(prop, @"^\d{1,}$"))
                .When(prop => !string.IsNullOrWhiteSpace(prop.StartColumnIndex))
                .WithMessage("A coluna inicial deve ser um número");

            RuleFor(prop => prop.StartRowIndex)
                .Must(prop => Regex.IsMatch(prop, @"^\d{1,}$"))
                .When(prop => !string.IsNullOrWhiteSpace(prop.StartRowIndex))
                .WithMessage("A linha inicial deve ser um número");


            //Length check

            RuleFor(prop => prop.Interval)
               .MaximumLength(4)
               .WithMessage("O intervalo deve ser um valor menor que '9999'");

            RuleFor(prop => prop.NumbersAmount)
               .MaximumLength(4)
               .WithMessage("A quantidade de números deve ser um valor menor que '9999'");

            //Greater than 0 check

            RuleFor(prop => prop.Interval)
                 .Must(prop=> int.Parse(prop)>0)
                 .When(prop => !string.IsNullOrWhiteSpace(prop.Interval)&&Regex.IsMatch(prop.Interval,@"^\d{1,}$"))
                 .WithMessage("O intervalo deve ter um valor positivo");

            RuleFor(prop => prop.ValueLimit)
                 .Must(prop => int.Parse(prop) > 0)
               .When(prop => !string.IsNullOrWhiteSpace(prop.ValueLimit) && Regex.IsMatch(prop.ValueLimit, @"^\d{1,}$"))
               .WithMessage("O valor limite deve ser positivo");

            RuleFor(prop => prop.NumbersAmount)
                 .Must(prop => int.Parse(prop) > 0)
               .When(prop => !string.IsNullOrWhiteSpace(prop.NumbersAmount) && Regex.IsMatch(prop.NumbersAmount, @"^\d{1,}$"))
               .WithMessage("A quantidade de números deve ter um valor positivo");

            RuleFor(prop => prop.StartRowIndex)
             .Must(prop => int.Parse(prop) > 0)
             .When(prop => !string.IsNullOrWhiteSpace(prop.StartRowIndex) && Regex.IsMatch(prop.StartRowIndex, @"^\d{1,}$"))
             .WithMessage("A linha inicial deve ter um valor positivo");

            RuleFor(prop => prop.StartColumnIndex)
                .Must(prop => int.Parse(prop) > 0)
                .When(prop => !string.IsNullOrWhiteSpace(prop.StartColumnIndex) && Regex.IsMatch(prop.StartColumnIndex, @"^\d{1,}$"))
                .WithMessage("A coluna inicial ter um valor positivo");


            RuleFor(prop => prop.SelectedFile)
                .SetValidator(new FileValidation());

           
        }
    }
}
