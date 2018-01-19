using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammar
{
    class ResultOutput
    {
        public void PrintPaths(List<string> paths)
        {
            foreach (var p in paths)
            {
                Console.WriteLine(p);
            }
        }

        public void WriteInFile(List<string> paths, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var p in paths)
                {
                    sw.WriteLine(p);
                }
            }
        }

        public int CountStart(List<string> paths)
        {
            int k = 0;
            foreach (var p in paths)
            {
                if (p.Contains("S,"))
                {
                    k++;
                }
            }
            return k;
        }
    }
}
