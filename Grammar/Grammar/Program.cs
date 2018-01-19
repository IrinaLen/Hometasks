using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphviz4Net.Dot.AntlrParser;
using QuickGraph.Algorithms.GraphColoring.VertexColoring;

namespace Grammar
{
    class Program
    {
        private static ResultOutput output = new ResultOutput();
        static void Main(string[] args)
        {
            if (args.Length != 1 && args.Length != 1 && args.Length != 3 && args.Length != 4)
            {
                Console.WriteLine("Invalid number of arguments. Try again.");
                return;
            }

            if (args.Length == 1)
            {
                if (args[0] == "-t")
                {
                    new Tests();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid number of arguments. Try again.");
                    return;
                }
            }

            string algoType = "", autPath = "", gramPath = "", resultPath = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Input algorithm name: Matrix, GLL, Union");
                algoType = Console.ReadLine();
                algoType = algoType.ToLower();

                while (algoType != "matrix" && algoType != "gll" && algoType != "union")
                {
                    Console.WriteLine("Not existed type. Try again...");
                    algoType = Console.ReadLine();
                    algoType = algoType.ToLower();
                }


                Console.WriteLine("Regular expression automat file path:");

                autPath = Console.ReadLine();
                while (!File.Exists(autPath))
                {
                    Console.WriteLine("Not existed file. Try again...");
                    autPath = Console.ReadLine();
                }

                Console.WriteLine("Grammar file path:");

                gramPath = Console.ReadLine();
                while (!File.Exists(gramPath))
                {
                    Console.WriteLine("Not existed file. Try again...");
                    gramPath = Console.ReadLine();
                }
                Console.WriteLine("Result file path:");

                resultPath = Console.ReadLine();
                while (resultPath != "" && !File.Exists(gramPath))
                {
                    Console.WriteLine("Not existed file. Try again...");
                    resultPath = Console.ReadLine();
                }
            }
            else
            {
                algoType = args[0];

                autPath = args[1];
                if (!File.Exists(autPath))
                {
                    Console.WriteLine("Not existed Regular expression automat file. Try again...");
                    return;
                }

                gramPath = args[2];
                if (!File.Exists(gramPath))
                {
                    Console.WriteLine("Not existed Grammar file. Try again...");
                    return;
                }

                resultPath = args.Length == 4 ? args[3] : "";
                if (resultPath != "" && !File.Exists(gramPath))
                {
                    Console.WriteLine("Not existed Result file. Try again...");
                    return;
                }

            }

            if (algoType.ToLower() == "matrix")
            {
                var matr = new MatrixAlgorithm(gramPath, autPath);
                if (resultPath == "") output.PrintPaths(matr.ReturnPaths());
                else output.WriteInFile(matr.ReturnPaths(), resultPath);
            }
            else if (algoType.ToLower() == "gll")
            {
                var gll = new GLLAlgorithm(gramPath, autPath);
                if (resultPath == "") output.PrintPaths(gll.ReturnPaths());
                else output.WriteInFile(gll.ReturnPaths(), resultPath);
            }
            else if (algoType.ToLower() == "union")
            {
                var matr = new UnionAutomats(gramPath, autPath);
                if (resultPath == "") output.PrintPaths(matr.ReturnPaths());
                else output.WriteInFile(matr.ReturnPaths(), resultPath);
            }
            else
            {
                Console.WriteLine("Invalid type. Try again.");
                return;
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

    }
}
