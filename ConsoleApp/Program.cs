namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            string fileCSV = "";

            Console.WriteLine("CSV DATA CONSOLE PRINTER\n");
            Console.Write("Paste here directory of folder where is your file: ");
            string dir = Console.ReadLine();

            List<string> csvFiles = new List<string>();

            #region DirPrepare

            bool dirCheck = false;
            while (!dirCheck)
            {
                csvFiles.Clear();
                if (Directory.Exists(dir))
                {
                    object[] files = Directory.GetFiles(dir);

                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file) == ".csv")
                        {
                            csvFiles.Add(file);
                        }
                    }
                    if (csvFiles.Count == 0)
                    {
                        Console.WriteLine("\nThe specified directory does not contains CSV files!\n");
                        Console.Write("Paste directory again: ");
                        dir = Console.ReadLine();
                    }
                    else
                    {
                        dirCheck = true;
                    }
                }
                else
                {
                    Console.WriteLine("\nThe specified directory does not exist.\n");
                    Console.Write("Paste directory again: ");
                    dir = Console.ReadLine();  
                }

            }
            #endregion

            #region FilePrepare
            Console.WriteLine();

            int fileCount = 0;
            foreach (string file in csvFiles)
            {
                fileCount++;
                Console.WriteLine($"{fileCount} | {Path.GetFileName(file)}");
            }
                
            //select file by number

            Console.WriteLine();
            Console.Write("Please write number of file you want to choose: ");
            string readString = Console.ReadLine();
            Console.WriteLine();

            //chcecking if string can be converted to int
            if (!String.IsNullOrEmpty(readString))
            {
                fileCSV = csvFiles[IntCorrection(readString, fileCount) - 1];
                Console.WriteLine($"\nSelected file: {fileCSV}\n");
            }

            #endregion

            reader.ImportAndPrintData(fileCSV);
        }


        public static int IntCorrection(string readString, int fileCount)
        {
            bool correct = false;
            if (readString != null)
            {
                while (!correct)
                {
                    try
                    {
                        int num = Convert.ToInt32(readString);
                        if (num <= fileCount)
                        {
                            correct = true;
                        }
                        else
                        {
                            Console.Write($"Number is over {fileCount}, try again! : ");
                            readString = Console.ReadLine();
                        }
                    }
                    catch
                    {
                        Console.Write("This isn't number, try again! : ");
                        readString = Console.ReadLine();
                    }
                }
            }
            return Convert.ToInt32(readString);
        }
    }
}
