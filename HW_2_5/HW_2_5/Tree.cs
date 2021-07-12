using System;
using System.Collections.Generic;
using System.Text;

namespace HW_2_5
{
    class Tree
    {
        public Tree Root { get; set; } = null;

        public int Value { get; private set; } = 0;
        public Tree LeftNode { get; set; } = null;
        public Tree RightNode { get; set; } = null;

        private Tree Parent { get; set; } = null;

        public Tree(int value)
        {
            Value = value;
            Parent = null;
        }

        public Tree FindNodeByValue(Tree root, int value)
        {
            if (root != null)
            {
                if (root.Value == value && (root.LeftNode == null || root.LeftNode.Value != value))
                    return root;

                Tree temp;

                if (root.Value >= value)
                {
                    temp = FindNodeByValue(root.LeftNode, value);
                    if (temp != null) return temp;
                }

                else
                {
                    temp = FindNodeByValue(root.RightNode, value);
                    if (temp != null) return temp;
                }
            }

            return null;
        }

        public bool AddNewNode(Tree root, int value)
        {
            if (root.Value >= value)
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = new Tree(value);
                    root.LeftNode.Parent = root;
                    return true;
                }
                else
                    AddNewNode(root.LeftNode, value);
            }
            else
            {
                if (root.RightNode == null)
                {
                    root.RightNode = new Tree(value);
                    root.RightNode.Parent = root;
                    return true;
                }
                else
                    AddNewNode(root.RightNode, value);
            }
            return false;
        }

        public bool RemoveNodeByValue(Tree root, int value)
        {
            Tree temp = FindNodeByValue(root, value);

            if (temp.LeftNode == null && temp.RightNode == null)
            {
                if (temp.Parent.Value >= temp.Value)
                {
                    temp.Parent.LeftNode = null;
                }
                else
                    temp.Parent.RightNode = null;

                temp.Parent = null;
                temp.Value = 0;

                return true;
            }
            return false;
        }

        public void DisplayTree(Tree root, String offset)
        {
            offset += "  ";

            if (root != null)
            {
                Console.WriteLine($"{offset} {root.Value}");
                DisplayTree(root.LeftNode, offset);
                DisplayTree(root.RightNode, offset);
            }
            return;
        }

        public Tree BFS(Tree tree, int value, int debug)
        {
            var queue = new Queue<Tree>();

            //Положить корень дерева в очередь.
            queue.Enqueue(tree);
            if (debug == 1) Console.WriteLine($"\nОчередь, искомое значение {value}") ;

            do
            {
                //Если очередь пуста, завершить работу алгоритма.
                if (queue.Count == 0)
                {
                    return null;
                }

                //Вынуть из очереди элемент. 
                Tree tempTree = queue.Dequeue();
                if (debug == 1) Console.WriteLine(tempTree.Value);

                //Если элемент искомый, вернуть его и завершить работу алгоритма.
                if (tempTree.Value == value)
                    return tempTree;

                //Положить все дочерние узлы элемента в очередь.
                if (tempTree.LeftNode != null)
                    queue.Enqueue(tempTree.LeftNode);

                if (tempTree.RightNode != null)
                    queue.Enqueue(tempTree.RightNode);

                //Вернуться к пункту 2.
            } while (true);

        }

        public Tree DFS(Tree tree, int value, int debug)
        {

            var stack = new Stack<Tree>();

            //Положить корень дерева в стек.
            stack.Push(tree);
            if (debug == 1) Console.WriteLine($"\nСтек, искомое значение {value}");

            do
            {
                //Если стек пуст, завершить работу алгоритма.
                if (stack.Count == 0)
                {
                    return null;
                }

                //Вынуть из стека элемент.
                Tree tempTree = stack.Pop();
                if (debug == 1) Console.WriteLine(tempTree.Value);

                //Если элемент искомый, вернуть его и завершить работу алгоритма.
                if (tempTree.Value == value)
                    return tempTree;

                //Положить все дочерние узлы элемента в стек.
                if (tempTree.LeftNode != null)
                    stack.Push(tempTree.LeftNode);

                if (tempTree.RightNode != null)
                    stack.Push(tempTree.RightNode);


                //Вернуться к пункту 2.
            } while (true);
        }
    }
}
