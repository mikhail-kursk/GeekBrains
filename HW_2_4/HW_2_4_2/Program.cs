using System;

namespace HW_2_4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree(10);

            tree.AddNewNode(tree, 7);
            tree.AddNewNode(tree, 1);
            tree.AddNewNode(tree, 2);
            tree.AddNewNode(tree, 15);
            tree.AddNewNode(tree, 9);
            tree.AddNewNode(tree, 22);
            tree.AddNewNode(tree, 9);
            tree.AddNewNode(tree, 1);
            tree.AddNewNode(tree, 1);
            tree.AddNewNode(tree, 1);

            tree.DisplayTree(tree, "");
            Console.WriteLine();


            Tree tree_2 = tree.FindNodeByValue(tree, 7);

            // Не удалится, так как есть потомки
            tree.RemoveNodeByValue(tree, 7);

            tree.DisplayTree(tree, "");
            Console.WriteLine();

            // Удалится, так как нет потомков
            tree.RemoveNodeByValue(tree, 1);

            tree.DisplayTree(tree, "");
        }
    }
}
