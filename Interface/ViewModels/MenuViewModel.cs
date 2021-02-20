using FluentValidation.Results;
using Interface.Validations;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Interface.ViewModels
{
    public class MenuViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                MenuViewModelValidation validator = new MenuViewModelValidation();

                ValidationResult result = validator.Validate(this);

                if (!result.IsValid && result.Errors.Any(err => err.PropertyName == columnName))
                {
                    error = result.Errors.Where(err => err.PropertyName == columnName).Select(err => err.ErrorMessage).FirstOrDefault();
                }

                return error;
            }
        }

        private FileInfo _selectedFile;
        public FileInfo SelectedFile { get => _selectedFile; set => _selectedFile = value; }

        private string _valueLimit;
        public string ValueLimit { get => _valueLimit; set => _valueLimit = value; }

        private string _interval;
        public string Interval { get => _interval; set => _interval = value; }

        private string _numbersAmount;
        public string NumbersAmount { get => _numbersAmount; set => _numbersAmount = value; }

        private string _startRowIndex;
        public string StartRowIndex { get => _startRowIndex; set => _startRowIndex = value; }

        private string _startColumnIndex;
        public string StartColumnIndex { get => _startColumnIndex; set => _startColumnIndex = value; }

        public string Error => string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}