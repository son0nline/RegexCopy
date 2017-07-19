using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexCopy
{
    class Program
    {
        static string pattern = @".*FTKTE00538001\t\[SQL\]\t\[START\].*";
        static void Main(string[] args)
        {
            List<string> rs = new List<string>();
            string line;
            Regex re = new Regex(pattern, RegexOptions.Compiled);
            Stopwatch stw = new Stopwatch();
            stw.Start();
            using(FileStream fs = new FileStream("aircore_0.log.txt", FileMode.Open, FileAccess.Read))
            {
                using(BufferedStream bs = new BufferedStream(fs))
                {
                    using(StreamReader sr = new StreamReader(bs, Encoding.GetEncoding("shift-jis")))
                    {
                        using(FileStream fsw = new FileStream("b.txt", FileMode.Append, FileAccess.Write))
                        {
                            using(StreamWriter sw = new StreamWriter(fsw, Encoding.GetEncoding("shift-jis")))
                            {
                                while((line = sr.ReadLine()) != null)
                                {
                                    //if(re.IsMatch(line))
                                    //{
                                    //    sw.WriteLine(line);
                                    //}

                                    //if(re.Match(line).Success)
                                    //{
                                    //    sw.WriteLine(line);
                                    //}

                                    // fastest way
                                    if(line.Contains(@"	[SQL]	[START]"))
                                    {
                                        sw.WriteLine(line);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            stw.Stop();


            TimeSpan ts = stw.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
