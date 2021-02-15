using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    public class MenuViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    default:
                        break;
                }

                return error;
            }
        }


        private int _valueLimit;
        public int ValueLimit { get; set; }

        private int _interval;
        public int Interval { get; set; }

        private int _numbersAmount;
        public int NumbersAmount { get; set; }

        public string Error => string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
