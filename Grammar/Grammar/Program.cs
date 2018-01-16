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
    	private const int N = 8;//количество тестов

        static void Main(string[] args)
        {
            if (args.Length > 0 && args.Length < 2 || args.Length > 2)
            {
                Console.WriteLine("Invalid number of arguments. Try again.");
                return;
            }
            if (args.Length == 2)
            {

                if (args[0].ToLower() == "-bigt")
                {
                    if (args[1].ToLower() == "-matrix")
                    {
                        BigTestsMatrix();
                        return;
                    }
                    if (args[1].ToLower() == "-gll")
                    {
                        BigTestGLL();
                        return;
                    }
                    if (args[1].ToLower() == "-union")
                    {
                        BigTestsUnion();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid second argument. Try again.");
                        return;
                    }
                }
                else if (args[0].ToLower() == "-smallt")
                {
                    if (args[1].ToLower() == "-matrix")
                    {
                        SmallTestsMatrix();
                        return;
                    }
                    if (args[1].ToLower() == "-gll")
                    {
                        SmallTestsGLL();
                        return;
                    }
                    if (args[1].ToLower() == "-union")
                    {
                        SmallTestsUnion();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid second argument. Try again.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid first argument. Try again.");
                    return;
                }
            }


            string autPath = "", gramPath = "", resultPath = "";
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


            Console.WriteLine("Input algorithm name: Matrix, GLL, Union");
            string algoType = Console.ReadLine();
            while (true)
            {
                if(algoType.ToLower() == "matrix")
                {
                    var matr = new MatrixAlgorithm(gramPath, autPath);
                    if(resultPath == "") PrintPaths(matr.ReturnPaths());
                    else WriteInFile(matr.ReturnPaths(), resultPath);
                    break;
                }
                else if (algoType.ToLower() == "gll")
                {
                    var gll = new GLLAlgorithm(gramPath, autPath);
                    if (resultPath == "") PrintPaths(gll.ReturnPaths());
                    else WriteInFile(gll.ReturnPaths(), resultPath);
                    break;
                }
                else if(algoType.ToLower() == "union")
                {
                    var matr = new MatrixAlgorithm(gramPath, autPath);
                    if (resultPath == "") PrintPaths(matr.ReturnPaths());
                    else WriteInFile(matr.ReturnPaths(), resultPath);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid type. Try again.");
                }
            }

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        //BigTests
        private static void BigTestsMatrix()
        {
            string[] automats =
            {
                "skos.dot",
                "generations.dot",
                "travel.dot",
                "univ-bench.dot",
                "atom-primitive.dot",
                "biomedical-mesure-primitive.dot",
                "foaf.dot",
                "people-pets.dot",
                "funding.dot",
                "wine.dot",
                "pizza.dot"
            };

            string[] grammars =
            {
                "Q1.txt",
                "Q2.txt",
                "Q3.txt"
            };

            using (StreamWriter sw = new StreamWriter(@"..\..\data\results\matrix_results.txt"))
            {
            	sw.WriteLine("Q1\t Q2\t Q3\t");

                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new MatrixAlgorithm(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                        sw.Write(CountStart(paths.ReturnPaths()) + "\t");
                        WriteInFile(paths.ReturnPaths(), @"..\..\data\results\matrix\result_" + a.Replace(".dot", "") + "_" + g.Replace(".txt", "") + ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
        }

        private static void BigTestGLL()
        {
            string[] automats =
            {
                "skos.dot",
                "generations.dot",
                "travel.dot",
                "univ-bench.dot",
                "atom-primitive.dot",
                "biomedical-mesure-primitive.dot",
                "foaf.dot",
                "people-pets.dot",
                "funding.dot",
                "wine.dot",
                "pizza.dot"
            };

            string[] grammars =
            {
                "Q1.dot",
                "Q2.dot",
                "Q3.dot"
            };

            using (StreamWriter sw = new StreamWriter(@"..\..\data\results\GLL_results.txt"))
            {
            	sw.WriteLine("Q1\t Q2\t Q3\t");

                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new GLLAlgorithm(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                        sw.Write(CountStart(paths.ReturnPaths()) + "\t");
                        WriteInFile(paths.ReturnPaths(), @"..\..\data\results\GLL\result_" + a.Replace(".dot", "") + "_" + g.Replace(".dot", "") + ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
        }

        private static void BigTestsUnion()
        {
            string[] automats =
            {
                "skos.dot",
                "generations.dot",
                "travel.dot",
                "univ-bench.dot",
                "atom-primitive.dot",
                "biomedical-mesure-primitive.dot",
                "foaf.dot",
                "people-pets.dot",
                "funding.dot",
                "wine.dot",
                "pizza.dot"
            };

            string[] grammars =
            {
                "Q1.dot",
                "Q2.dot",
                "Q3.dot"
            };


            using (StreamWriter sw = new StreamWriter(@"..\..\data\results\union_results.txt"))
            {
            	sw.Write("Q1\t Q2\t Q3\t");
                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new UnionAutomats(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                        sw.Write(CountStart(paths.ReturnPaths()) + "\t");
                        WriteInFile(paths.ReturnPaths(), @"..\..\data\results\union\result_" + a.Replace(".dot", "") + "_" + g.Replace(".dot", "") + ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
        }


        //SmallTests
        private static void SmallTestsMatrix()
        {
            for (int i = 1; i <= N; i++)
            {
                string a = "t" + i.ToString() + ".dot";
                string g = "t" + i.ToString() + ".txt";
                Console.Write(a + " " + g + "\t");
                var paths = new MatrixAlgorithm(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                PrintPaths(paths.ReturnPaths());
            }
        }

        private static void SmallTestsGLL()
        {
            for (int i = 1; i <= N; i++)
            {
                string a = "t" + i.ToString() + ".dot";               
                string g = "t" + i.ToString() + "g.dot";
                Console.Write(a + " " + g + "\t");
                var paths = new GLLAlgorithm(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                PrintPaths(paths.ReturnPaths());
            }
        }

        private static void SmallTestsUnion()
        {
            for (int i = 1; i <= N; i++)
            {
                string a = "t" + i.ToString() + ".dot";
                string g = "t" + i.ToString() + "g.dot";

                Console.Write(a + " " + g + "\t");
                var paths = new UnionAutomats(@"..\..\data\grammars\" + g, @"..\..\data\automats\" + a);
                PrintPaths(paths.ReturnPaths());
            }

        }
        private static void PrintPaths(List<string> paths)
        {
            foreach (var p in paths)
            {
                Console.WriteLine(p);
            }
        }

        private static void WriteInFile(List<string> paths, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var p in paths)
                {
                    sw.WriteLine(p);
                }
            }
        }

        private static int CountStart(List<string> paths)
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
