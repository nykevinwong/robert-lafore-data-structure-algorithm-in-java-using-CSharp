
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgo
{


    class Program
    {
        public static void PrintPathList(IList<Vertex[]> lists)
        {
            for (int j = 0; j < lists.Count; j++)
            {
                Vertex[] vs = lists[j];
                for (int i = 0; i < vs.Length; i++)
                {
                    if (i != 0) Console.Write("=>");
                    Console.Write(vs[i].label);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            AdjacentMatrixGraph g = new AdjacentMatrixGraph();
            g.AddVertex('A');
            g.AddVertex('B');
            g.AddVertex('C');
            g.AddVertex('D');
            g.AddVertex('E');
            g.AddVertex('F');
            g.AddVertex('G');
            g.AddVertex('H');
            g.AddVertex('I');
            g.AddEdge('A', 'B');
            g.AddConnection('B', 'C');
            g.AddConnection('B', 'D');
            g.AddConnection('B', 'E');
            g.AddConnection('C', 'H');
            g.AddConnection('D', 'I');
            g.AddConnection('A', 'F');
            g.AddConnection('A', 'G');

            g.Print();

            Console.WriteLine("\nDFS Path:");
            IList<Vertex[]> list = g.getDFSPaths();
            PrintPathList(list);

            Console.WriteLine("\nBFS Path:");
            IList<Vertex[]> bfsList = new List<Vertex[]>();
            bfsList.Add(g.getBFSPaths().ToArray());
            PrintPathList(bfsList);

            Console.WriteLine("\nConnectivity (all paths):");
            IList<Vertex[]> connectivity = g
                .getConnectivity();
            PrintPathList(connectivity);

            AdjacentMatrixGraph g2 = new AdjacentMatrixGraph();
            g2.AddVertex('A');
            g2.AddVertex('B');
            g2.AddVertex('C');
            g2.AddVertex('D');
            g2.AddVertex('E');
            g2.AddEdge('A', 'B');
            g2.AddEdge('A', 'C');
            g2.AddEdge('A', 'D');
            g2.AddEdge('A', 'E');
            g2.AddEdge('B', 'C');
            g2.AddEdge('B', 'D');
            g2.AddEdge('B', 'E');
            g2.AddEdge('C', 'D');
            g2.AddEdge('C', 'E');
            g2.AddEdge('D', 'E');

            g2.Print();

            Console.WriteLine("\nMTS:");
            IList<Vertex[]> mts = g2.getMTS();
            PrintPathList(mts);


            Console.ReadKey();
        }
    }
}
