using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammar
{
    class Tests
    {
    	private int[] mustBig, mustSmall;
        public void Start()
        {

            mustBig = new[]
            {
                810, 1, 32,
                2164, 0, 19,
                2499, 63, 31,
                2540, 81, 12,
                15454, 122, 3,
                15156, 2871, 0,
                4118, 10, 46,
                9472, 37, 36,
                17634, 1158, 18,
                66572, 133, 1215,
                56195, 1262, 9520
            };



            mustSmall = new[]
            {
                4, 1, 2, 2, 15, 20, 123, 1, 12
            };
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // создаем объект
            swatch.Start(); // старт

            BigTestsMatrix();
            Console.WriteLine("--------------------");
            BigTestGLL();
            Console.WriteLine("--------------------");

            BigTestsUnion();
            Console.WriteLine("--------------------");


            SmallTestsMatrix();
            Console.WriteLine("--------------------");

            SmallTestsGLL();
            Console.WriteLine("--------------------");

            SmallTestsUnion();
            Console.WriteLine("--------------------");


            swatch.Stop(); // стоп
            Console.WriteLine(swatch.Elapsed); // выводим результат в консоль
        }

        private const int N = 9; //количество тестов
        private ResultOutput output = new ResultOutput();
        //BigTests
        private void BigTestsMatrix()
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
            List<int> res = new List<int>();
            using (StreamWriter sw = new StreamWriter(@".\data\results\matrix_results.txt"))
            {
                sw.WriteLine("Q1\t Q2\t Q3\t");

                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new MatrixAlgorithm(@".\data\grammars\" + g, @".\data\automats\" + a);
                        sw.Write(output.CountStart(paths.ReturnPaths()) + "\t");
                        res.Add(output.CountStart(paths.ReturnPaths()));
                        output.WriteInFile(paths.ReturnPaths(),
                            @".\data\results\matrix\result_" + a.Replace(".dot", "") + "_" + g.Replace(".txt", "") +
                            ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustBig) ? "Test matrix is OK" : "Test matrix is failed");
        }

        private void BigTestGLL()
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
            List<int> res = new List<int>();

            using (StreamWriter sw = new StreamWriter(@".\data\results\GLL_results.txt"))
            {
                sw.WriteLine("Q1\t Q2\t Q3\t");

                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new GLLAlgorithm(@".\data\grammars\" + g, @".\data\automats\" + a);
                        sw.Write(output.CountStart(paths.ReturnPaths()) + "\t");
                        res.Add(output.CountStart(paths.ReturnPaths()));
                        output.WriteInFile(paths.ReturnPaths(),
                            @".\data\results\GLL\result_" + a.Replace(".dot", "") + "_" + g.Replace(".dot", "") +
                            ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustBig) ? "Test GLL is OK" : "Test GLL is failed");

        }

        private void BigTestsUnion()
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

            List<int> res = new List<int>();

            using (StreamWriter sw = new StreamWriter(@".\data\results\union_results.txt"))
            {
                sw.Write("Q1\t Q2\t Q3\t");
                foreach (var a in automats)
                {
                    foreach (var g in grammars)
                    {
                        Console.Write(a + " " + g + "\n");
                        var paths = new UnionAutomats(@".\data\grammars\" + g, @".\data\automats\" + a);
                        sw.Write(output.CountStart(paths.ReturnPaths()) + "\t");
                        res.Add(output.CountStart(paths.ReturnPaths()));
                        output.WriteInFile(paths.ReturnPaths(),
                            @".\data\results\union\result_" + a.Replace(".dot", "") + "_" + g.Replace(".dot", "") +
                            ".txt");
                    }
                    sw.Write(a + "\r\n");
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustBig) ? "Test union is OK" : "Test union is failed");

        }

        //SmallTests
        private void SmallTestsMatrix()
        {
            List<int> res = new List<int>();

            using (StreamWriter sw = new StreamWriter(@".\data\results\matrix_result_small.txt"))
            {
                for (int i = 1; i <= N; i++)
                {
                    
                    string a = "t" + i.ToString() + ".dot";
                    string g = "t" + i.ToString() + ".txt";
                    Console.Write(a + " " + g + "\n");
                    var paths = new MatrixAlgorithm(@".\data\grammars\" + g, @".\data\automats\" + a);
                    output.PrintPaths(paths.ReturnPaths());
                    res.Add(output.CountStart(paths.ReturnPaths()));
                    sw.Write("T" + i.ToString() + "\t" + output.CountStart(paths.ReturnPaths()));
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustSmall) ? "Test matrix is OK" : "Test matrix is failed");

        }

        private void SmallTestsGLL()
        {
            List<int> res = new List<int>();

            using (StreamWriter sw = new StreamWriter(@".\data\results\GLL_result_small.txt"))
            {
                for (int i = 1; i <= N; i++)
                {
                    string a = "t" + i.ToString() + ".dot";
                    string g = "t" + i.ToString() + "g.dot";
                    Console.Write(a + " " + g + "\n");
                    var paths = new GLLAlgorithm(@".\data\grammars\" + g, @".\data\automats\" + a);
                    output.PrintPaths(paths.ReturnPaths());
                    res.Add(output.CountStart(paths.ReturnPaths()));
                    sw.Write("T" + i.ToString() + "\t" + output.CountStart(paths.ReturnPaths()));
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustSmall) ? "Test GLL is OK" : "Test GLL is failed");

        }

        private void SmallTestsUnion()
        {
            List<int> res = new List<int>();

            using (StreamWriter sw = new StreamWriter(@".\data\results\union_result_small.txt"))
            {
                for (int i = 1; i <= N; i++)
                {
                    string a = "t" + i.ToString() + ".dot";
                    string g = "t" + i.ToString() + "g.dot";

                    Console.Write(a + " " + g + "\n");
                    var paths = new UnionAutomats(@".\data\grammars\" + g, @".\data\automats\" + a);
                    output.PrintPaths(paths.ReturnPaths());
                    res.Add(output.CountStart(paths.ReturnPaths()));
                    sw.Write("T" + i.ToString() + "\t" + output.CountStart(paths.ReturnPaths()));
                }
            }
            Console.WriteLine(res.ToArray().SequenceEqual(mustSmall) ? "Test Union is OK" : "Test Union is failed");

        }

        public void LocalTests()
        {
            List<Tuple<int, int, int>> resList = new List<Tuple<int, int, int>>();
            for (int a = 1; a <= 12; a++)
            {
                for (int g = 1; g <= 10; g++)
                {
                    Console.Write(a + " " + g + "\n");
                    var m = new MatrixAlgorithm(@"D:\UnitTests\grammars\" + g + ".txt", @"D:\UnitTests\graphs\" + a + ".dot");
                    int rm = output.CountStart(m.ReturnPaths());
                    var gl = new GLLAlgorithm(@"D:\UnitTests\grammars\" + g + ".dot", @"D:\UnitTests\graphs\" + a + ".dot");
                    int rgl = output.CountStart(gl.ReturnPaths());
                    var u = new UnionAutomats(@"D:\UnitTests\grammars\" + g + ".dot", @"D:\UnitTests\graphs\" + a + ".dot");
                    int ru = output.CountStart(u.ReturnPaths());
                    resList.Add(Tuple.Create(rm, rgl, ru));
                    if (ru == rgl && rgl == rm) Console.WriteLine("-----------\nOK\n-----------\n");
                    else Console.WriteLine("-----------\nFail\n-----------\n");
                }
            }
        }
    }
}
