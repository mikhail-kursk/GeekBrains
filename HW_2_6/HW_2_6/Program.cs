using System;

namespace HW_2_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            Graph.Node Node_1 = new Graph.Node(1, graph);
            Graph.Node Node_2 = new Graph.Node(2, graph);
            Graph.Node Node_3 = new Graph.Node(3, graph);
            Graph.Node Node_4 = new Graph.Node(4, graph);
            Graph.Node Node_5 = new Graph.Node(5, graph);
            Graph.Node Node_6 = new Graph.Node(6, graph);
            Graph.Node Node_7 = new Graph.Node(7, graph);
            Graph.Node Node_8 = new Graph.Node(8, graph);
            Graph.Node Node_9 = new Graph.Node(9, graph);



            /*  1   2   3   4   5   6   7   8   9
            
            1   -   8   4   10          9

            2   8   -       18  3   1       6

            3   4       -                       15

            4   10  18      -                   3

            5       3           -   2       14

            6       1           2   -

            7   9                       -   2   4

            8       6           14      2   -

            9           15  3           4       -    */

            graph.AddNewBiDirectionalRelatilation(Node_1, Node_2, 8);
            graph.AddNewBiDirectionalRelatilation(Node_1, Node_3, 4);
            graph.AddNewBiDirectionalRelatilation(Node_1, Node_4, 10);
            graph.AddNewBiDirectionalRelatilation(Node_1, Node_7, 9);
            
            graph.AddNewBiDirectionalRelatilation(Node_2, Node_4, 18);
            graph.AddNewBiDirectionalRelatilation(Node_2, Node_5, 3);
            graph.AddNewBiDirectionalRelatilation(Node_2, Node_6, 1);
            graph.AddNewBiDirectionalRelatilation(Node_2, Node_8, 6);
            
            graph.AddNewBiDirectionalRelatilation(Node_3, Node_9, 15);
            
            graph.AddNewBiDirectionalRelatilation(Node_4, Node_9, 3);
            
            graph.AddNewBiDirectionalRelatilation(Node_5, Node_6, 2);
            graph.AddNewBiDirectionalRelatilation(Node_5, Node_8, 14);

            graph.AddNewBiDirectionalRelatilation(Node_7, Node_8, 2);
            graph.AddNewBiDirectionalRelatilation(Node_7, Node_9, 4);

            Console.WriteLine(graph.BFS(Node_1));
            Console.WriteLine(graph.DFS(Node_1));

            Console.WriteLine(graph.Dijkstra(Node_4, Node_5));

        }
    }
}
