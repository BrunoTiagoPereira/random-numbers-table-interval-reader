using FluentValidation.Results;
using Models.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Models
{
    public class RNT
    {
        public DataTable SourceTable { get; set; }

        /// <summary>
        /// Amount of numbers to take
        /// </summary>
        public int NumbersLimitAmount { get; set; }

        public int Interval { get; set; }
        public int ValueLimit { get; set; }
        public int StartColumnIndex { get; set; }
        public int StartRowIndex { get; set; }

        public static event EventHandler<int> OnPopulate;

        /// <summary>
        /// Constructor for the Random Numbers Table
        /// </summary>
        /// <param name="file">File object with the table data</param>
        /// <param name="numbersAmount">Amount of numbers to take</param>
        /// <param name="interval">Interval that will be used</param>
        /// <param name="valueLimit">Value limit to fetch numbers</param>
        public RNT(FileInfo file, int numbersAmount, int interval, int valueLimit, int startColumnIndex = 0, int startRowIndex = 0)
        {
            if (!File.Exists(file.FullName)) { throw new FileNotFoundException("The specified file doesn't exists."); }

            RNTValidation validator = new RNTValidation();

            ValidationResult result = new ValidationResult();

            string errorMessage;

            SourceTable = new DataTable();

            NumbersLimitAmount = numbersAmount;

            Interval = interval;

            ValueLimit = valueLimit;

            StartColumnIndex = startColumnIndex;

            StartRowIndex = startRowIndex;

            PopulateSourceTable(file);

            result = validator.Validate(this);

            if (!result.IsValid)
            {
                errorMessage = result.Errors
                       .Select(err => err.ErrorMessage)
                       .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
                throw new InvalidDataException(errorMessage);
            }
        }

        /// <summary>
        /// Populate the source table with the values in file
        /// </summary>
        /// <param name="file">file object to read</param>
        private void PopulateSourceTable(FileInfo file)
        {
            DataRow dr;

            List<string> fileLines = GetFileLines(file);

            OnPopulate?.Invoke(this, fileLines.Count);

            int maxLineLength = fileLines.Max(line => line.ToArray().Length);

            char[] fileLineSplit;

            //Add enough columns to source table
            for (int i = 0; i < maxLineLength; i++) SourceTable.Columns.Add(new DataColumn(i.ToString(), typeof(string)));

            //Populate the source transforming each position to char array and get the single numbers
            for (int i = 0; i < fileLines.Count; i++)
            {
                dr = SourceTable.NewRow();

                fileLineSplit = fileLines[i].ToArray();

                dr.ItemArray = fileLineSplit
                    .Select(num => num.ToString())
                    .Cast<object>()
                    .ToArray();

                SourceTable.Rows.Add(dr);
            }
        }

        /// <summary>
        /// read the file data as a stream and returns with each line as a position of the string list
        /// </summary>
        /// <param name="file">file to be extract of</param>
        /// <returns>string list containing each line</returns>
        private List<string> GetFileLines(FileInfo file)
        {
            string data = null;

            List<string> fileLines = new List<string>();

            using (StreamReader reader = new StreamReader(new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                while ((data = reader.ReadLine()) != null) fileLines.Add(data);
            }

            return fileLines;
        }

        public List<int> GetNumbersList()
        {
            return Search();
        }

        //private List<int> RecursiveSearch(int rowIndex = 0, int columnIndex = 0, List<int> result = null)
        //{
        //    if (result == null) result = new List<int>();

        //    if (result.Count == NumbersLimitAmount) return result;

        //    StringBuilder numberResult = new StringBuilder();

        //    for (int i = 0; i < Interval; i++)
        //    {
        //        if (SourceTable.Columns.Count == columnIndex)
        //        {
        //            columnIndex = 0;
        //            rowIndex += 1;
        //        }

        //        if (SourceTable.Rows.Count == rowIndex) rowIndex = 0;

        //        numberResult.Append(SourceTable.Rows[rowIndex][columnIndex]);

        //        columnIndex++;
        //    }

        //    if (int.Parse(numberResult.ToString()) <= ValueLimit) result.Add(int.Parse(numberResult.ToString()));

        //    return Search(rowIndex,columnIndex,result);
        //}
        private List<int> Search(List<int> result = null)
        {
            int rowIndex = this.StartRowIndex;
            int columnIndex = this.StartColumnIndex;

            if (result == null) result = new List<int>();

            StringBuilder numberResult = new StringBuilder();

            while (result.Count != NumbersLimitAmount)
            {
                for (int i = 0; i < Interval; i++)
                {
                    if (SourceTable.Columns.Count == columnIndex)
                    {
                        columnIndex = 0;
                        rowIndex += 1;
                    }

                    if (SourceTable.Rows.Count == rowIndex) rowIndex = 0;

                    numberResult.Append(SourceTable.Rows[rowIndex][columnIndex]);

                    columnIndex++;
                }

                if (int.Parse(numberResult.ToString()) <= ValueLimit) result.Add(int.Parse(numberResult.ToString()));

                numberResult = new StringBuilder();
            }

            return result;
        }
    }
}