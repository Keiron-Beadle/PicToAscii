using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicToAscii
{
    class Program
    {
        static List<string> filesList = new List<string>();

        static void Main(string[] args)
        {
            DisplayMenu();
            AsciiArtCreator ascii = new AsciiArtCreator(GetMenuResult());

            ascii.Go("output.txt");
        }

        private static string GetMenuResult()
        {
            bool validInput = false;
            do
            {
                string input = Console.ReadLine();
                int num = 0;
                try
                {
                    num = int.Parse(input);
                }
                catch
                { }
                return filesList[--num];
            } while (!validInput);
        }

        private static void DisplayMenu()
        {
            filesList.AddRange(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg", SearchOption.TopDirectoryOnly));
            filesList.AddRange(Directory.GetFiles(Directory.GetCurrentDirectory(), "*.png", SearchOption.TopDirectoryOnly));
            for (int i = 0; i < filesList.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + filesList[i]);
            }
        }
    }
}
