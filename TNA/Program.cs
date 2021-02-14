using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNA
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileText = File.ReadAllText("init.txt");

            int interval = 0;

            int numbersLimit = 0;

            int valueLimit = 0;

            int amountInLine = 0;

            char[] array = fileText.ToCharArray();

            List<string> result = new List<string>();


            Console.WriteLine("Intervalo:");

            interval = int.Parse(Console.ReadLine());

            Console.WriteLine("Quantidade de números:");

            numbersLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("Valor limite:");

            valueLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("Números em uma linha:");

            amountInLine = int.Parse(Console.ReadLine());


            DataTable dt = new DataTable();



            result = GetNumeros(interval, numbersLimit,valueLimit, array);

            Console.WriteLine(result.Aggregate((a, b) => $"{a}{Environment.NewLine}{b}"));

            Console.ReadKey();








        }

        private static List<string> GetNumeros(int interval,int numbersLimit, int valueLimit, char[]array, int index=0, List<string> numbersList = null)
        {

            int newIndex = index + interval;

            if (numbersList == null) numbersList = new List<string>();

            if (numbersList.Count == numbersLimit) return numbersList;

            StringBuilder numberResult = new StringBuilder();



            for (int i = index; i < newIndex; i++)
            {
                numberResult.Append(array[i]);

            }

            if (int.Parse(numberResult.ToString()) <= valueLimit) numbersList.Add(numberResult.ToString());

            return GetNumeros(interval, numbersLimit, valueLimit, array, newIndex, numbersList);


        }
    }
}
