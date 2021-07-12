using System;
using System.Collections.Generic;

namespace HW_2_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree(10);

            int treeNodes = 20;      // Создаем 20 узлов и листьев дерева
            int guaranteeValue = 15; // Гарантированно присутствующее значение
            int debug = 1;           // Включает отображение дерева и значений которые вынимаются на каждом шаге из дерева

            for (int i = 0; i < treeNodes - 2; i++)
            {
                Random random = new Random();
                tree.AddNewNode(tree, random.Next(100));
            }

            tree.AddNewNode(tree, guaranteeValue);
            if (debug == 1) tree.DisplayTree(tree, "");

            Tree result;

            result = tree.BFS(tree, 204, debug);
            Check(result, 204);

            result = tree.BFS(tree, 15, debug);
            Check(result, 15);

            result = tree.DFS(tree, 204, debug);
            Check(result, 204);

            result = tree.DFS(tree, 15, debug);
            Check(result, 15);

        }

        public static void Check(Tree check, int value)
        {
            if (check == null)
            {
                Console.WriteLine($"Значение {value} не найдено");
                return;
            }

            if (check.Value == value)
                Console.WriteLine($"Значение {value} найдено");
            else
                Console.WriteLine($"Значение {value} не найдено");
        }
    }
}
