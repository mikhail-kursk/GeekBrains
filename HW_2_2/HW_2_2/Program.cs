using System;

namespace HW_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Node Node_1 = new Node(1, null, null);
            Node Node_2 = new Node(2, null, Node_1);
            Node Node_3 = new Node(3, null, Node_2);
            Node Node_4 = new Node(4, null, Node_3);
            Node Node_5 = new Node(5, null, Node_4);
            Node Node_6 = new Node(6, null, Node_5);

            Console.WriteLine(Node_1.GetCount()); // 6
            // Порядок после выполнения
            // 1
            // 2
            // 3
            // 4
            // 5
            // 6

            Node_6.AddNode(7);
            Node_4.AddNode(8);
            Console.WriteLine(Node_1.GetCount()); // 8
            // Порядок после выполнения
            // 1
            // 2
            // 3
            // 4
            // 5
            // 6
            // 7
            // 8

            Node_1.AddNodeAfter(Node_4, 1);
            Console.WriteLine(Node_1.GetCount()); // 8 - так как перенос нод
            // Порядок после выполнения
            // 1
            // 2
            // 4
            // 3
            // 5
            // 6
            // 7
            // 8

            Node_1.RemoveNode(4);
            Console.WriteLine(Node_1.GetCount()); // 7
            // Порядок после выполнения
            // 1
            // 2
            // 4
            // 3
            // 6
            // 7
            // 8

            Node_1.RemoveNode(Node_2);
            Console.WriteLine(Node_1.GetCount()); // 6
            // Порядок после выполнения
            // 1
            // 4
            // 3
            // 6
            // 7
            // 8

            Node t_1 = Node_1.FindNode(8);
            Node t_2 = Node_1.FindNode(15);
            Node t_3 = Node_1.FindNode(3);

            Console.ReadLine();

        }
    }
}
