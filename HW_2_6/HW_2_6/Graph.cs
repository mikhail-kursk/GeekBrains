using System;
using System.Collections.Generic;

namespace HW_2_6
{
    class Graph
    {

        public List<Node> Nodes { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public class Node //Вершина
        {
            public int Value { get; set; }
            public List<Edge> Edges { get; set; } //исходящие связи

            public Node(int value, Graph graph)
            {
                Value = value;
                graph.Nodes.Add(this);
                Edges = new List<Edge>();
            }
        }

        public class Edge //Ребро
        {
            public int Weight { get; set; } //вес связи
            public Node Node { get; set; } // узел, на который связь ссылается

            public Edge(int weight, Node node)
            {
                Weight = weight;
                Node = node;
            }
        }

        public void AddNewBiDirectionalRelatilation(Node node_1, Node node_2, int weight)
        {
            Graph.Edge edge_1_2 = new Graph.Edge(weight, node_2);
            node_1.Edges.Add(edge_1_2);

            Graph.Edge edge_2_1 = new Graph.Edge(weight, node_1);
            node_2.Edges.Add(edge_2_1);
        }

        public bool BFS(Node node)
        {
            if (node == null)
                return false;

            var queue = new Queue<Node>();

            bool[] nodes = new bool[Nodes.Count];

            queue.Enqueue(node);

            bool processing = true;
            do
            {
                var nodeFromQueue = queue.Dequeue();
                nodes[nodeFromQueue.Value - 1] = true;
                Console.WriteLine($"Node =  {nodeFromQueue.Value}");

                foreach (Edge edge in nodeFromQueue.Edges)
                {
                    if (nodes[edge.Node.Value - 1] == false)
                    {
                        nodes[edge.Node.Value - 1] = true;
                        queue.Enqueue(edge.Node);
                    }
                }

                if (queue.Count == 0)
                    processing = false;

            } while (processing);

            foreach (Node currentNode in Nodes)
            {
                if (nodes[currentNode.Value - 1] == false)
                    return false;
            }

            return true;
        }

        public bool DFS(Node node)
        {
            if (node == null)
                return false;

            var stack = new Stack<Node>();

            bool[] nodes = new bool[Nodes.Count];

            stack.Push(node);

            bool processing = true;
            do
            {
                var nodeFromQueue = stack.Pop();
                nodes[nodeFromQueue.Value - 1] = true;
                Console.WriteLine($"Node =  {nodeFromQueue.Value}");

                foreach (Edge edge in nodeFromQueue.Edges)
                {
                    if (nodes[edge.Node.Value - 1] == false)
                    {
                        nodes[edge.Node.Value - 1] = true;
                        stack.Push(edge.Node);
                    }
                }

                if (stack.Count == 0)
                    processing = false;

            } while (processing);

            foreach (Node currentNode in Nodes)
            {
                if (nodes[currentNode.Value - 1] == false)
                    return false;
            }

            return true;
        }

        public string Dijkstra(Node source, Node target)
        {
            if (source == null || target == null)
                return "Неверный ввод";

            var stack = new Stack<Node>();

            int[] nodesDist = new int[Nodes.Count];

            for (int i = 0; i < Nodes.Count; i++)
            {
                nodesDist[i] = int.MaxValue;
            }
            nodesDist[source.Value - 1] = 0;

            stack.Push(source);

            bool processing = true;
            do
            {
                var nodeFromQueue = stack.Pop();
                Console.WriteLine($"Node =  {nodeFromQueue.Value}");

                foreach (Edge edge in nodeFromQueue.Edges)
                {
                    if (edge.Weight + nodesDist[nodeFromQueue.Value - 1] < nodesDist[edge.Node.Value - 1])
                    {
                        nodesDist[edge.Node.Value - 1] = edge.Weight + nodesDist[nodeFromQueue.Value - 1];
                        stack.Push(edge.Node);
                    }
                }

                if (stack.Count == 0)
                    processing = false;

            } while (processing);

            var currentNode = target;
            string result = target.Value + "  ";

            processing = true;
            do
            {
                foreach (Edge edge in currentNode.Edges)
                {
                    if (nodesDist[edge.Node.Value - 1] == nodesDist[currentNode.Value - 1] - edge.Weight)
                    {
                        currentNode = edge.Node;
                        result += currentNode.Value + "  ";
                        break;
                    }
                }

                if (currentNode == source)
                    processing = false;


            } while (processing);

            char[] charArray = result.ToCharArray();
            Array.Reverse(charArray);
            result = "Кратчайший путь" + new string (charArray);

            return result + " длительность пути " + nodesDist[target.Value - 1];
        }


    }
}
