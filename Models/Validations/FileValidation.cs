using FluentValidation;
using System.IO;

namespace Models.Validations
{
    public class FileValidation : AbstractValidator<FileInfo>
    {
        public FileValidation()
        {
            RuleFor(prop => prop.Extension.ToLower())
                .Equal(@".txt")
                .WithMessage("A extensão do arquivo deve ser '.txt'");

            RuleFor(prop => prop.Length)
                .GreaterThan(0)
                .WithMessage("O tamanho do arquivo deve ser maior que '0'");
        }
    }
}