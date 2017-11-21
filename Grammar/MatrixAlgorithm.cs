using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Grammar
{
    class MatrixAlgorithm
    {
        private List<string>[,] matrix;
        private int N;
        private Dictionary<string, string> toFrom; // AB <- S 
        private Dictionary<string, string> terms; //a <- S
        private string epsilon = "";
        public MatrixAlgorithm()
        {
            toFrom = new Dictionary<string, string>();
            terms = new Dictionary<string, string>();
            ParseGrammar("grammar.txt");
            ParseAvtomat("avtomat.dot");
            TermToNonterm();
            AddEpsilon();
            Floid();
            PrintAndReturPaths();
        }

        private void TermToNonterm()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        foreach (var el in matrix[i, j].ToArray())
                        {
                            if (terms.ContainsKey(el))
                            {
                                matrix[i, j].Add(terms[el]);
                                matrix[i, j].Remove(el);
                            }
                        }
                    }
                }
            }
        }

        private void AddEpsilon()
        {
            if (epsilon != "")
            {
                for (int i = 0; i < N; i++)
                {
                    if (matrix[i, i] != null)
                    {
                         matrix[i, i].Add(epsilon);
                    }
                    else
                    {
                        matrix[i, i] = new List<string> {epsilon};
                    }
                }
            }
        }
        private void ParseGrammar(string path)
        {
            using (StreamReader sr = new StreamReader(@path))
            {
                string line;
                string[] parsedLine;
                while ((line = sr.ReadLine()) != null)
                {
                    parsedLine = line.Split();//S, ->, B, C
                    if (parsedLine[2][0] == '_')//epsilon- переход
                    {
                        epsilon = parsedLine[0];
                    }
                    else if ('a' <= parsedLine[2][0] && parsedLine[2][0] <= 'z')
                    {
                        terms.Add(parsedLine[2], parsedLine[0]);
                    }
                    else
                    {
                        toFrom.Add(parsedLine[2] + parsedLine[3], parsedLine[0]);
                    }
                }
            }
        }

        private void ParseAvtomat(string path)
        {
            GraphData.GraphData graph;
            using (StreamReader sr = new StreamReader(@path))
            {
                graph = DotParser.parse(sr.ReadToEnd());
            }
            N = graph.Nodes.Count;
            matrix = new List<string>[N, N];

            foreach (var n in graph.Edges)
            {
               // Console.Write("{0} - {1} ===", n.Key.Item1, n.Key.Item2);
                int i, j;
                i = Convert.ToInt32(n.Key.Item1);
                j = Convert.ToInt32(n.Key.Item2);
                matrix[i, j] = new List<string>();
                foreach (var v in n.Value)
                {
                    matrix[i, j].Add(v["label"]);
                  //  Console.Write(" {0}, ", v["label"]);
                }
               // Console.WriteLine();
            }
        }

       
        private void Floid()
        {
            bool wasChanged = false;
            do
            {
                wasChanged = false;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        for (int k = 0; k < N; k++)
                        {
                            if (matrix[i, k] == null || matrix[k, j] == null)
                                continue;
                            foreach (string ik in matrix[i, k].ToArray())
                            {
                                foreach (string kj in matrix[k, j].ToArray())
                                {
                                    string ij = ik + kj;
                                    if (toFrom.ContainsKey(ij))
                                    {
                                        string term = toFrom[ij];
                                        if (matrix[i, j] != null)
                                        {
                                            if (matrix[i, j].Contains(term))
                                                continue;
                                            else
                                            {
                                                matrix[i, j].Add(term);
                                                wasChanged = true;
                                            }
                                        }
                                        else
                                        {
                                            matrix[i, j] = new List<string> {term};
                                            wasChanged = true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            } while (wasChanged);
        }

        public List<string> PrintAndReturPaths()
        {
            List<string> paths = new List<string>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        foreach (var nontrem in matrix[i, j])
                        {
                            string p = "(" + i.ToString() + ", " + nontrem + ", " + j.ToString() + ")";
                            Console.Write("{0};  ", p);
                            paths.Add(p);
                        }
                    }
                }
            }
            return paths;
        }

    }
}