﻿using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TNA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RNT.OnPopulate += Source_OnPopulate;

            try
            {
                RNT source2 = new RNT(new FileInfo("init.txt"), 50, 4, 1050, startRowIndex: 25);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            RNT source = new RNT(new FileInfo("init.txt"), 50, 4, 1050);

            List<int> teste = source.GetNumbersList();

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

            result = GetNumeros(interval, numbersLimit, valueLimit, array);

            Console.WriteLine(result.Aggregate((a, b) => $"{a}{Environment.NewLine}{b}"));

            Console.ReadKey();
        }

        private static void Source_OnPopulate(object sender, int e)
        {
            Console.WriteLine(e);
        }

        private static List<string> GetNumeros(int interval, int numbersLimit, int valueLimit, char[] array, int index = 0, List<string> numbersList = null)
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