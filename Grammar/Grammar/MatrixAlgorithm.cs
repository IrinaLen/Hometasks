using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Grammar
{
    class MatrixAlgorithm
    {
        private List<string>[,] matrix;
        private int N;
        private Dictionary<string, List<string>> toFrom; // AB <- S 
        private Dictionary<string, List<string>> terms; //a <- S
        private List<string> epsilon;

        public MatrixAlgorithm(string grammarPath, string automatPath)
        {
            toFrom = new Dictionary<string, List<string>>();
            terms = new Dictionary<string, List<string>>();
            epsilon = new List<string>();
            ParseGrammar(grammarPath);
            if (terms.Count == 0) return;
            ParseAvtomat(automatPath);
            Console.WriteLine("Parsed");
            TermToNonterm();
            AddEpsilon();
            Floid();
            Console.WriteLine("Finish");
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
                                foreach (var e in terms[el])
                                {
                                    matrix[i, j].Add(e);
                                    matrix[i, j].Remove(el);
                                }
                            }
                            else
                            {
                                matrix[i, j].Remove(el);
                            }
                        }
                    }
                }
            }
        }

        private void AddEpsilon()
        {
            if (epsilon.Count > 0)
            {
                for (int i = 0; i < N; i++)
                {
                    if (matrix[i, i] != null)
                    {
                        foreach (var e in epsilon)
                        {
                            matrix[i, i].Add(e);
                        }
                    }
                    else
                    {
                        matrix[i, i] = new List<string> (epsilon);
                    }
                }
            }
        }
        private void ParseGrammar(string path)
        {
            if (path.Contains(".dot"))
            {
                Console.WriteLine("Invalid type of Grammar file. Try again");
                return;
            }
            using (StreamReader sr = new StreamReader(@path))
            {
                string line;
                string[] parsedLine;
                while ((line = sr.ReadLine()) != null)
                {
                    parsedLine = line.Split(':');
                    string leftPart = parsedLine[0].Replace(" ", "");
                    string[] rightPart = parsedLine[1].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);//S, ->, B, C
                    if (String.Equals(rightPart[0], "eps"))//epsilon- переход

                    parsedLine = line.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);//S, :, B, C
                    if (String.Equals(parsedLine[2], "eps"))//epsilon- переход
                    {
                        epsilon.Add(leftPart);
                    }
                    else if (!('A' <= rightPart[0][0] && rightPart[0][0] <= 'Z'))
                    {
                        if (terms.ContainsKey(rightPart[0]))
                        {
                            terms[rightPart[0]].Add(leftPart);
                        }
                        else
                        {
                            terms.Add(rightPart[0], new List<string> { leftPart });
                        }
                    }
                    else
                    {
                        string key = rightPart[0] + " " + (rightPart.Length == 1 ? "" : rightPart[1]);
                        if (toFrom.ContainsKey(key))
                        {
                            toFrom[key].Add(leftPart);
                        }
                        else
                        {
                            toFrom.Add(key, new List<string>{leftPart});
                        }
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
                int i, j;
                i = Convert.ToInt32(n.Key.Item1);
                j = Convert.ToInt32(n.Key.Item2);
                matrix[i, j] = new List<string>();
                foreach (var v in n.Value)
                {
                    matrix[i, j].Add(v["label"]);
                }
            }
        }
       
        private void Floid()
        {
            int count = 1;
            bool wasChanged;
            do
            {
                Console.WriteLine("while: " + count.ToString());
                count++;
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
                                    string ij = ik + " " + kj;
                                    if (toFrom.ContainsKey(ij))
                                    {
                                        foreach (string term in toFrom[ij])
                                        {
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
                }
                Console.Write("\n");

            } while (wasChanged);
        }
        
        public List<string> ReturnPaths()
        {
            List<string> paths = new List<string>();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        foreach (var nonterm in matrix[i, j])
                        {
                            string p = i.ToString() + "," + nonterm + "," + j.ToString();
                            paths.Add(p);
                        }
                    }
                }
            }
            return paths;
        }
    }
}
