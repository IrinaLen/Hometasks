using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Graphviz4Net.Dot;
using Graphviz4Net.Dot.AntlrParser;
using Graphviz4Net.Graphs;

namespace Grammar
{
    public class UnionAutomats
    {
        private List<string>[,] unionAutomat;
        private Dictionary<int, string> grammarStart;
        private List<int> grammarFinal;
        private Dictionary<string, int> idForPair; // a,g
        private Dictionary<int, Dictionary<string, int>> grammarPaths;
        private Dictionary<int, Dictionary<string, List<int>>> automatPaths;
        private int n = 0; //кол-во состояний в автомате


        public UnionAutomats(string gr, string aut)
        {
            grammarPaths = new Dictionary<int, Dictionary<string, int>>();
            automatPaths = new Dictionary<int, Dictionary<string, List<int>>>();
            grammarStart = new Dictionary<int, string>();
            grammarFinal = new List<int>();
            idForPair = new Dictionary<string, int>();
            Parse(gr, aut);
            Union();
        }

        private void Parse(string gr, string aut)
        {
            GraphData.GraphData automat;
            DotGraph<int> grammar;
            // Console.Write("{0} - {1} ===", n.Key.Item1, n.Key.Item2);

            using (StreamReader sr = new StreamReader(aut))
            {
                automat = DotParser.parse(sr.ReadToEnd());
                foreach (var el in automat.Edges)
                {
                    int x1 = Convert.ToInt32(el.Key.Item1);
                    int x2 = Convert.ToInt32(el.Key.Item2);
                    foreach (var v in el.Value)
                    {
                        string lab = v["label"];

                        if (automatPaths.ContainsKey(x1))
                        {
                            if (automatPaths[x1].ContainsKey(lab))
                            {
                                automatPaths[x1][lab].Add(x2);
                            }
                            else
                            {
                                if (automatPaths[x1] == null)
                                    automatPaths[x1] = new Dictionary<string, List<int>> {{lab, new List<int> {x2}}};
                                else automatPaths[x1].Add(lab, new List<int> {x2});
                            }
                        }
                        else
                        {
                            automatPaths.Add(x1, new Dictionary<string, List<int>> {{lab, new List<int> {x2}}});
                        }
                    }
                }
            }

            using (StreamReader sr = new StreamReader(gr))
            {
                grammar = AntlrParserAdapter<int>.GetParser().Parse(@sr.ReadToEnd());
                foreach (var el in grammar.VerticesEdges)
                {
                    int x1 = el.Source.Id;
                    int x2 = el.Destination.Id;
                    string lab = el.Attributes["label"];

                    if (grammarPaths.ContainsKey(x1))
                    {
                        grammarPaths[x1].Add(lab, x2);
                    }
                    else
                    {
                        grammarPaths.Add(x1, new Dictionary<string, int> { { lab, x2 } });
                    }
                }
            }

            int k = 0;
            foreach (var g in grammar.Vertices)
            {
                foreach (var a in automat.Nodes)
                {
                    var s = a.Key.ToString() + "," + g.Id.ToString();
                    if(idForPair.ContainsKey(s)) continue;
                    idForPair.Add(s, k);
                    k++;
                }

                if (g.Attributes.ContainsKey("color") && g.Attributes["color"] == "green")
                {
                    grammarStart.Add(g.Id, g.Attributes["label"]);
                }
                if (g.Attributes.ContainsKey("shape") && g.Attributes["shape"] == "doublecircle")
                {
                    grammarFinal.Add(g.Id);
                }
            }
            n = automat.Nodes.Count;
            unionAutomat = new List<string>[k, k];
        }

        private void Union()
        {
            bool hasChanged = false;
            HashSet<Tuple<int, string, int>> addToAut = new HashSet<Tuple<int, string, int>>();
            Stack<Tuple<int, int, string, int, int>> stack = new Stack<Tuple<int, int, string, int, int>>(); //start(a,g), labelб finish
            HashSet< Tuple < int, int, string, int, int>> history = new HashSet<Tuple<int, int, string, int, int>>();
               int currAutState, currGramState;
            int k = 1;
            do
            {
                Console.WriteLine("While: " + k.ToString());
                k++;
                foreach (var g in grammarStart.Keys)
                {
                    for (int a = 0; a < n; a++)
                    {
                        currGramState = g;
                        currAutState = a;
                        //start==final
                        if (grammarFinal.Contains(currGramState))
                        {
                            AddToUnion(a, g, grammarStart[g], currAutState, currGramState);
                            if (!addToAut.Contains(Tuple.Create(a, grammarStart[g], currAutState)))
                                addToAut.Add(Tuple.Create(a, grammarStart[g], currAutState));
                        }
                        do
                        {
                            if (stack.Count > 0)
                            {
                                var curSt = stack.Pop();
                                history.Add(curSt);
                                AddToUnion(curSt.Item1, curSt.Item2, curSt.Item3, curSt.Item4, curSt.Item5);
                                currAutState = curSt.Item4;
                                currGramState = curSt.Item5;
                            }
                            if (automatPaths.ContainsKey(currAutState))//не тупиковая
                            {
                                foreach (var l in automatPaths[currAutState].Keys)
                                {
                                    if (grammarPaths.ContainsKey(currGramState) &&
                                        grammarPaths[currGramState].ContainsKey(l))
                                    {
                                        foreach (var p in automatPaths[currAutState][l])
                                        {
                                            var t = Tuple.Create(currAutState, currGramState, l, p,
                                                grammarPaths[currGramState][l]);
                                            if (!history.Contains(t))
                                            stack.Push(Tuple.Create(currAutState, currGramState, l, p,
                                                grammarPaths[currGramState][l]));
                                        }
                                    }
                                }
                            }
                            if (grammarFinal.Contains(currGramState))
                            {
                                AddToUnion(a, g, grammarStart[g], currAutState, currGramState);
                                if(!addToAut.Contains(Tuple.Create(a, grammarStart[g], currAutState))) addToAut.Add(Tuple.Create(a, grammarStart[g], currAutState));
                            }

                        } while (stack.Count > 0);
                        history.Clear();
                    }
                }
                AddToAutomath(ref addToAut, ref hasChanged);
            } while (hasChanged);
        }

        private void AddToUnion(int a1, int g1, string lab, int a2, int g2)
        {
            int i, j;
            i = idForPair[a1.ToString() + "," + g1.ToString()];
            j = idForPair[a2.ToString() + "," + g2.ToString()];
            if (unionAutomat[i, j] == null)
            {
                unionAutomat[i, j] = new List<string> {lab};
            }
            else
            {
                if (!unionAutomat[i, j].Contains(lab))
                {
                    unionAutomat[i, j].Add(lab);
                }
            }
        }

        private void AddToAutomath(ref HashSet<Tuple<int, string, int>> addToAut, ref bool hasChanged)
        {
            hasChanged = false;
            int x1, x2;
            string s;
            foreach (var elem in addToAut)
            {
                x1 = elem.Item1;
                x2 = elem.Item3;
                s = elem.Item2;
                if (automatPaths.ContainsKey(x1))
                {
                    if (automatPaths[x1].ContainsKey(s))
                    {
                        if (automatPaths[x1][s].Contains(x2))
                        {
                            continue;
                        }
                        else
                        {
                            automatPaths[x1][s].Add(x2);
                            hasChanged = true;
                        }
                    }
                    else
                    {
                        automatPaths[x1].Add(s, new List<int> {x2});
                        hasChanged = true;
                    }
                }
                else
                {
                    automatPaths.Add(x1, new Dictionary<string, List<int>> {{s, new List<int> {x2}}});
                }
            }
        }

        public List<string> ReturnPaths()
        {
            List<string> paths = new List<string>();
            List<string> notterms = grammarStart.Values.ToList();
            foreach (var i in automatPaths.Keys)
            {
                foreach (var nonterm in automatPaths[i].Keys)
                {
                    foreach (var j in automatPaths[i][nonterm])
                    {
                        if (notterms.Contains(nonterm))
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