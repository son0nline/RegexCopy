using System;
using System.Collections.Generic;
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

            using(FileStream fs = new FileStream("a.txt", FileMode.Open, FileAccess.Read))
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
                                    if(re.IsMatch(line))
                                    {
                                        sw.WriteLine(line);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
