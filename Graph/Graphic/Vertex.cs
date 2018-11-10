
namespace GraphAlgo
{
    public class Vertex
    {
        public char label;
        public int index;
        public bool visited;
        public Vertex(char lab, int index)
        {
            this.label = lab;
            this.index = index;
        }
    }
}
