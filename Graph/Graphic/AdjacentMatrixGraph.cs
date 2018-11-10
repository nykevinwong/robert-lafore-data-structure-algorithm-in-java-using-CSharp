
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgo
{
    public class AdjacentMatrixGraph
    {
        const int VERTEX_NUM = 20;
        Vertex[] vertexList = new Vertex[VERTEX_NUM];
        int vertexCount = 0;
        int[,] adjMatrix = new int[VERTEX_NUM, VERTEX_NUM];
        Dictionary<char, Vertex> char2Vertex = new Dictionary<char, Vertex>();

        public AdjacentMatrixGraph()
        {
        }

        public void AddVertex(char lab)
        {
            Vertex v = new Vertex(lab, vertexCount);
            vertexList[vertexCount++] = v;
            char2Vertex[lab] = v;
        }

        public void ResetVertices()
        {
            for (int i = 0; i < vertexCount; i++)
                vertexList[i].visited = false;
        }

        public void AddEdge(int start, int end)
        {
            adjMatrix[start, end] = 1;
            adjMatrix[end, start] = 1;
        }

        public void AddEdge(char start, char end)
        {
            if (!char2Vertex.ContainsKey(start) || !char2Vertex.ContainsKey(end))
                throw new InvalidOperationException("Invalid vertex/vertices");

            AddEdge(char2Vertex[start].index, char2Vertex[end].index);
        }

        // directed
        public void AddConnection(char start, char end)
        {
            AddConnection(start - 'A', end - 'A');
        }

        public void AddConnection(int start, int end)
        {
            adjMatrix[start, end] = 1;
        }

        public void Print()
        {
            for (int y = 0; y < vertexCount; y++)
            {
                if (y == 0)
                {
                    Console.Write("  ");

                    for (int x = 0; x < vertexCount; x++)
                        Console.Write(vertexList[x].label + " ");
                    Console.WriteLine();
                }

                Console.Write(vertexList[y].label + " ");

                int c = 0;
                for (int x = 0; x < vertexCount && adjMatrix[y, x] == 0; x++) c++;

                if (c == vertexCount) Console.Write("."); // display . if whole line is 0
                else
                {
                    for (int x = 0; x < vertexCount; x++)
                    {
                        Console.Write(adjMatrix[y, x] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }

        protected bool isLastNode(int index)
        {
            int count = 0;
            for (int x = 0; x < vertexCount; x++)
            {
                if (index == x) continue; // skip since source and dest can not be the same
                if (adjMatrix[index, x] == 1)
                { // connected neighor/childrens
                    count++;
                }
            }

            return count == 0;
        }

        protected Vertex getUnvisitedNeighbor(int index)
        {
            for (int x = 0; x < vertexCount; x++)
            {
                if (index == x) continue; // skip since source and dest can not be the same
                if (adjMatrix[index, x] == 1)
                { // connected neighor/childrens
                    Vertex neighbor = vertexList[x];
                    if (neighbor.visited == false)
                        return neighbor;
                }
            }
            return null;
        }

        public IList<Vertex> getBFSPaths()
        {
            IList<Vertex> list = new List<Vertex>();
            Queue<Vertex> q = new Queue<Vertex>();
            Vertex v = vertexList[0];
            v.visited = true;
            q.Enqueue(v);

            while (q.Count > 0)
            {
                v = q.Dequeue();
                list.Add(v);
                Vertex found;
                while ((found = getUnvisitedNeighbor(v.index)) != null)
                {
                    q.Enqueue(found);
                    found.visited = true;
                }

            }

            ResetVertices();
            return list;
        }


        // print all paths, work for directed graph only
        public IList<Vertex[]> getDFSPaths()
        {
            IList<Vertex[]> lists = new List<Vertex[]>();
            Stack<Vertex> s = new Stack<Vertex>();
            s.Push(vertexList[0]);
            vertexList[0].visited = true;

            while (s.Count > 0)
            {
                Vertex v = s.Peek();
                Vertex neighbor = getUnvisitedNeighbor(v.index);

                if (neighbor == null) // not found 
                {
                    Vertex tmp = s.Peek();

                    // not the first node 
                    if (tmp.index != 0 && isLastNode(tmp.index))
                        lists.Add(s.Reverse().ToArray());

                    s.Pop();
                }
                else
                {

                    neighbor.visited = true;
                    s.Push(neighbor);
                }
            }

            ResetVertices();
            return lists;
        }

        // print all paths, work for directed graph only
        public IList<Vertex[]> getConnectivity()
        {
            IList<Vertex[]> lists = new List<Vertex[]>();
            int curStart = 0;

            while (curStart < vertexCount)
            {
                Stack<Vertex> s = new Stack<Vertex>();
                s.Push(vertexList[curStart]);
                vertexList[curStart].visited = true;

                while (s.Count > 0)
                {
                    Vertex v = s.Peek();
                    Vertex neighbor = getUnvisitedNeighbor(v.index);

                    if (neighbor == null) // not found 
                    {
                        Vertex tmp = s.Peek();

                        // not the first node 
                        if (tmp.index != 0 && isLastNode(tmp.index))
                            lists.Add(s.Reverse().ToArray());

                        s.Pop();
                    }
                    else
                    {

                        neighbor.visited = true;
                        s.Push(neighbor);
                    }
                }

                ResetVertices();
                curStart++;
            }

            return lists;
        }


        // print all paths, work for directed graph only
        public IList<Vertex[]> getMTS()
        {
            IList<Vertex[]> lists = new List<Vertex[]>();
            Stack<Vertex> s = new Stack<Vertex>();
            s.Push(vertexList[0]);
            vertexList[0].visited = true;

            while (s.Count > 0)
            {
                Vertex v = s.Peek();
                Vertex neighbor = getUnvisitedNeighbor(v.index);

                if (neighbor == null) // not found 
                {
                    s.Pop();
                }
                else
                {
                    neighbor.visited = true;
                    s.Push(neighbor);
                    lists.Add(new Vertex[] { v, neighbor });
                }
            }

            ResetVertices();
            return lists;
        }


    }

}