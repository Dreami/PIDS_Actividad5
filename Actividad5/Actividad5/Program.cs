using FileOutputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Actividad5
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(Outputs.getAllFiles());
            FileInfo[] Files = d.GetFiles("*.txt");

            string output_path5 = @"C:\Users\maple\Documents\9° Semester\CS13309_Archivos_HTML\a5_matricula.txt";
            string output;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (FileInfo file in Files)
            {
                output = "";
                var watchEach = System.Diagnostics.Stopwatch.StartNew();
                string htmlContent = File.ReadAllText(file.FullName);
                htmlContent.Trim();
                List<string> sortedWords = new List<string>();
                Dictionary<string, int> repetitions = new Dictionary<string, int>();

                string[] eachWord = htmlContent.Split(' ');
                string lowercase;
                try
                {
                    foreach (string word in eachWord)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            lowercase = word.ToLower();
                            if (repetitions.ContainsKey(lowercase))
                            {
                                repetitions[lowercase]++;
                            }
                            else
                            {
                                repetitions.Add(lowercase, 1);
                            }
                        }
                    }
                    
                } catch (ArgumentNullException argExc)
                {
                    Console.WriteLine(argExc.StackTrace);
                }
                catch (KeyNotFoundException keyNotFoundExc)
                {
                    Console.WriteLine(keyNotFoundExc.StackTrace);
                }
                var list = repetitions.Keys.ToList();
                list.Sort();

                foreach (var key in list)
                {
                    output += key + " \t" + repetitions[key] + "\n";
                }

                output += "\n" + file.Name + " sorted in\t" + watchEach.Elapsed.TotalMilliseconds.ToString() + " ms";
                Console.WriteLine(output);
                watchEach.Stop();
                Outputs.output_print(output_path5, output);
            }

            output = "\nAll files sorted in\t" + watch.Elapsed.TotalMilliseconds.ToString() + " ms";
            Console.WriteLine(output);
            watch.Stop();
            Outputs.output_print(output_path5, output);

            Console.Read();
        }
    }
}
