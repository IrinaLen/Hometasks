using Graphviz4Net.Dot;
using Graphviz4Net.Dot.AntlrParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Grammar
{
    class GLLAlgorithm
    {
        private Dictionary<int, string> grammarStart;
        private Dictionary<int, string> grammarFinal;
        Dictionary<string, List<int>> nonterms;
        private Dictionary<int, List<Tuple<string, int>>> grammarPaths;
        private Dictionary<int, List<Tuple<string, int>>> automatPaths;
        private List<Tuple<string, int>> gssVertex;
        private Dictionary<int, List<Tuple<int, int>>> gssEdges;
        private List<string> paths;
        private int n = 0; //кол-во состояний в автомате


        public GLLAlgorithm(string gr, string aut)
        {
            grammarPaths = new Dictionary<int, List<Tuple<string, int>>>();
            automatPaths = new Dictionary<int, List<Tuple<string, int>>>();
            nonterms = new Dictionary<string, List<int>>();
            grammarStart = new Dictionary<int, string>();
            grammarFinal = new Dictionary<int, string>();
            gssVertex = new List<Tuple<string, int>>();
            gssEdges = new Dictionary<int, List<Tuple<int, int>>>();
            paths = new List<string>();
            Parse(gr, aut);
            GLL();
       }

        public List<string> ReturnPaths()
        {
            return paths;
        }

        private void Parse(string gr, string aut)
        {
            GraphData.GraphData automat;
            DotGraph<int> grammar;

            using (StreamReader sr = new StreamReader(aut))
            {
                try
                {
                    automat = DotParser.parse(sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                foreach (var el in automat.Edges)
                {
                    int x1 = Convert.ToInt32(el.Key.Item1);
                    int x2 = Convert.ToInt32(el.Key.Item2);
                    foreach (var v in el.Value)
                    {
                        string lab = v["label"];

                        if (automatPaths.ContainsKey(x1))
                        {
                            automatPaths[x1].Add(Tuple.Create(lab, x2));
                        }
                        else
                        {
                            automatPaths.Add(x1, new List<Tuple<string, int>> {Tuple.Create(lab, x2)});
                        }
                    }
                }
            }

            using (StreamReader sr = new StreamReader(gr))
            {
                try
                {
                    grammar = AntlrParserAdapter<int>.GetParser().Parse(@sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                foreach (var el in grammar.VerticesEdges)
                {
                    int x1 = el.Source.Id;
                    int x2 = el.Destination.Id;
                    string lab = el.Attributes["label"];

                    if (grammarPaths.ContainsKey(x1))
                    {
                        grammarPaths[x1].Add(Tuple.Create(lab, x2));
                    }
                    else
                    {
                        grammarPaths.Add(x1, new List<Tuple<string, int>> {Tuple.Create(lab, x2)});
                    }
                }
            }

            foreach (var g in grammar.Vertices)
            {
                if (g.Attributes.ContainsKey("color") && g.Attributes["color"] == "green")
                {
                    var l = g.Attributes["label"];
                    grammarStart.Add(g.Id, l);
                    if (nonterms.ContainsKey(l))
                    {
                        nonterms[l].Add(g.Id);
                    }
                    else
                    {
                        nonterms.Add(l, new List<int> {g.Id});
                    }
                }
                if (g.Attributes.ContainsKey("shape") && g.Attributes["shape"] == "doublecircle")
                {
                    grammarFinal.Add(g.Id, g.Attributes["label"]);
                }
            }

            n = automat.Nodes.Count;
        }

       
        
        private void GLL()
        {
            int currGram, currAut, currStack;
            Stack<Tuple<int, int, int>> workList = new Stack<Tuple<int, int, int>>(); //P_a, P_g, P_s
            HashSet<Tuple<int, int, int>> history = new HashSet<Tuple<int, int, int>>(); //P_a, P_g, P_s
            Dictionary<int, List<int>> poped = new Dictionary<int, List<int>>();

            int id = 0;      
            for (int a = 0; a < n; a++)
            {
                foreach (var gr in grammarStart.Keys)
                {
                    if (!gssVertex.Contains(Tuple.Create(grammarStart[gr], a)))
                    {
                        gssVertex.Add(Tuple.Create(grammarStart[gr], a));
                        workList.Push(Tuple.Create(a, gr, id));
                        id++;
                    }
                }
            }
            int k = 0;
            while (workList.Count > 0)
            {
                var config = workList.Pop();
                if(history.Contains(config)) continue;
                currAut = config.Item1;
                currGram = config.Item2;
                currStack = config.Item3;
                history.Add(config);
                k++;
                if(k % 1000 == 0) Console.WriteLine("Iteration: " + k.ToString());
              
                if (grammarPaths.ContainsKey(currGram))
                {
                    foreach (var i in grammarPaths[currGram])
                    {
                        string gramLab = i.Item1;
                        int gramEnd = i.Item2;
                        if (nonterms.ContainsKey(gramLab))
                        {
                            int stackEnd = AddVertex(gramLab, currAut);
                            AddEdge(stackEnd, gramEnd, currStack);

                            foreach (var start in nonterms[gramLab])
                            {
                                workList.Push(Tuple.Create(currAut, start, stackEnd));
                            }

                            if (poped.ContainsKey(stackEnd))
                            {
                                foreach (var pa in poped[stackEnd])
                                {
                                    workList.Push(Tuple.Create(pa, gramEnd, currStack));
                                }
                            }
                        }
                        if (automatPaths.ContainsKey(currAut))
                        {
                            foreach (var j in automatPaths[currAut])
                            {
                                string autLab = j.Item1;
                                int autEnd = j.Item2;
                                //если терминал
                                if (gramLab == autLab)
                                {
                                    workList.Push(Tuple.Create(autEnd, gramEnd, currStack));
                                }
                            }
                        }
                    }
                }

                if (grammarFinal.ContainsKey(currGram)) //указатель на финале
                {
                     var popedNode = gssVertex[currStack];


                    if (poped.ContainsKey(currStack))
                    {
                        if(poped[currStack].Contains(currAut)) continue;
                        poped[currStack].Add(currAut);
                    }
                    else
                    {
                        poped.Add(currStack, new List<int> {currAut});
                    }

                    AddPath(popedNode.Item2, popedNode.Item1, currAut);

                    if (gssEdges.ContainsKey(currStack))
                    {
                        foreach (var edge in gssEdges[currStack])
                        {
                            workList.Push(Tuple.Create(currAut, edge.Item1, edge.Item2));
                        }
                    }
                }
            }
       }

       

        private int AddVertex(string nt, int pA)
        {
            var t = Tuple.Create(nt, pA);
            if (gssVertex.Contains(t))
            {
                return gssVertex.IndexOf(t);
            }
            else
            {
                gssVertex.Add(t);
                return gssVertex.Count - 1;
            }
        }

        private void AddEdge(int pS1, int gr, int pS2)
        {
            var t = Tuple.Create(gr, pS2);
            if (gssEdges.ContainsKey(pS1))
            {
                if (gssEdges[pS1].Contains(t))
                {
                    return;
                }
                else
                {
                    gssEdges[pS1].Add(t);
                }
            }
            else
            {
                gssEdges.Add(pS1, new List<Tuple<int, int>> {t});
            }
        }

        private void AddPath(int from, string nonTerm, int to)
        {
            string s = from.ToString() + "," + nonTerm + "," + to.ToString();
            if (!paths.Contains(s))
            {
                paths.Add(s);
            }
        }
    }
}