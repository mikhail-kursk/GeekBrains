using System;
using System.Collections.Generic;
using System.Text;

namespace HW_2_2
{
    public class Node : ILinkedList
    {
        public bool isDefined = false;

        public static Node FirstNode;
        public static Node LastNode;
        public static int NumberOfElements = 0;


        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }

        public Node(int value, Node nextNode, Node prevNode)
        {
            Value = value;
            NextNode = nextNode;
            PrevNode = prevNode;

            LastNode = this;
            isDefined = true;

            NumberOfElements++;

            if (PrevNode != null && PrevNode.isDefined)
            {
                PrevNode.NextNode = this;
            }

            if (PrevNode == null)
            {
                FirstNode = this;
            }
        }

        public int GetCount() // возвращает количество элементов в списке
        { return NumberOfElements; }

        public void AddNode(int value)  // добавляет новый элемент списка
        {
            LastNode = new Node(value, null, LastNode);
        }

        public void AddNodeAfter(Node node, int value) // добавляет новый элемент списка после определённого элемента
        {
            if (value <= Node.NumberOfElements && Node.NumberOfElements > 1)
            {
                Node currentNode = FindNodeByNumber(value);

                // Отвязывем ноду откуда мы ее перемещаем
                if (node.PrevNode != null)
                    if (node.NextNode != null)
                        node.PrevNode.NextNode = node.NextNode;
                    else
                        node.PrevNode.NextNode = null;
                else
                {
                    if (node.NextNode != null)
                      FirstNode = node.NextNode;
                }


                if (node.NextNode != null)
                    if (node.PrevNode != null)
                        node.NextNode.PrevNode = node.PrevNode;
                    else
                        node.NextNode.PrevNode = null;
                else
                {
                    if (node.PrevNode != null)
                        LastNode = node.PrevNode;
                }
                // Привязываем ноду в новом месте
                Node nextNode = currentNode.NextNode;

                currentNode.NextNode = node;
                node.PrevNode = currentNode;

                nextNode.PrevNode = node;
                node.NextNode = nextNode;


            }
        }

        public void RemoveNode(int index) // удаляет элемент по порядковому номеру
        {
            Node removeNode = FindNodeByNumber(index);
            RemoveNode(removeNode);
        }
        public void RemoveNode(Node node) // удаляет указанный элемент
        {
            if (node.PrevNode != null)
            {
                if (node.NextNode != null)
                    node.PrevNode.NextNode = node.NextNode;
                else
                    node.PrevNode.NextNode = null;
            }
            else
            {
                if (node.NextNode != null)
                    node.NextNode.PrevNode = null;
            }

            if (node.NextNode != null)
            {
                if (node.PrevNode != null)
                    node.NextNode.PrevNode = node.PrevNode;
                else
                    node.PrevNode.NextNode = null;
            }
            else
            {
                if (node.PrevNode != null)
                    node.PrevNode.NextNode = null;
            }
            NumberOfElements--;

        }
        public Node FindNode(int searchValue) // ищет элемент по его значению
        {
            Node currentNode = Node.FirstNode;

            while (true)
            {
                if (currentNode.Value == searchValue)
                    return currentNode;

                if (currentNode.NextNode != null)
                    currentNode = currentNode.NextNode;
                else
                    return null;
            }
        }

        private Node FindNodeByNumber(int number)
        {
            Node currentNode = Node.FirstNode;
            var currentNodeNumber = 0;

            while (number != currentNodeNumber)
            {
                currentNode = currentNode.NextNode;
                currentNodeNumber++;
            }

            return currentNode;
        }


    }


}
