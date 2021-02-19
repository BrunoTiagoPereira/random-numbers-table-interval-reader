using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Exporting
{
    public class ExportInterface
    {
       
        public List<int> NumbersToExport { get; set; }

        public FileExtension Extension { get; set; }

        public string FilePath { get; set; }

        public ExportInterface(List<int> numbersToExport, FileExtension extension, string filePath)
        {
            NumbersToExport = numbersToExport;
            Extension = extension;
            FilePath = filePath;
        }

        public void Export()
        {

            switch (Extension)
            {
                case FileExtension.Txt:
                    ExportToTxtFile();
                    break;
              
            }
        }

        private void ExportToTxtFile()
        {
            string fileData = NumbersToExport
                .Select(num => num.ToString())
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
            File.WriteAllText(FilePath, fileData);

        }

        public enum FileExtension
        {
            Txt
        }
    }
}
