namespace TestNodes
{
    public class Node
    {
        public Point[] In { get; private set; }
        public Point[] Out { get; private set; }

        public int Id { get; private set; }

        public Node(int id, Point[] pointsIn, Point[] pointsOut) 
        {
            Id = id;
            In = pointsIn;
            Out = pointsOut;

            for (int i = 0; i < In.Length; i++)
                In[i].ValueChanged += Execute;
        }
        public void Destroy()
        {
            foreach (var node in In)
                node.Close();

            foreach (var node in Out)
                node.Close();
        }

        public virtual void Execute() { }
    }
}