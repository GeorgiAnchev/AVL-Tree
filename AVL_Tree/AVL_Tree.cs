using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public partial class Tree : IEnumerable
    {
        private int size;

        /// <summary>
        /// Size of the tree.
        /// </summary>
        public int Size
        {
            get
            {
                return this.size;
            }
        }

        private Node root;

        public Tree()
        {
            this.size = 0;
            this.root = null;
        }

        /// <summary>
        /// Inserts a node.
        /// </summary>
        /// <param name="data">The data of the node.</param>
        /// <returns>True if insertion is successful, false if the node is already in the tree.</returns>
        public bool Insert(int data)
        {
            if(this.root == null)
            {
                this.root = new Node(data);
                return true;
            }
            else
            {
                Node node = this.root;
                while(node != null)
                {
                    int compare = data.CompareTo(node.Data);
                    if (compare < 0)
                    {
                        Node left = node.Left;

                        if (left == null)
                        {
                            node.Left = new Node(data, node);
                            InsertBalance(node, 1);
                            this.size++;
                            return true;
                        }
                        else
                        {
                            node = left;//we move to the left and continue with next iteration
                        }
                    }
                    else if (compare > 0)
                    {
                        Node right = node.Right;
                        if (right == null)
                        {
                            node.Right = new Node(data, node);
                            InsertBalance(node, -1);
                            this.size++;
                            return true;
                        }
                        else
                        {
                            node = right;//we mode to the right and continue with next iteration
                        }
                    }
                    else 
                    { 
                        return false; 
                    }
                }
            }
            return false;//unreachable
        }

        private void InsertBalance(Node node, int balance)
        {
            while (node != null)//we are traversing the tree 
            {
                node.Balance += balance;
                balance = node.Balance;

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)//the left side is longer with 2 levels
                {
                    if (node.Left.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }
                    return;
                }
                else if (balance == -2)//the right side is longer with 2 levels
                {
                    if (node.Right.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }
                    return;
                }

                Node parent = node.Parent;
                if (parent != null)
                {
                    balance = (parent.Right == node ? -1 : 1);
                }

                node = parent; // we move to the parent
            }
        }

        private Node RotateRight(Node node)
        {
            Node left = node.Left;
            Node leftRight = left.Right;
            Node parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if(leftRight != null)
            {
                leftRight.Parent = node;
            }
            if(node == this.root)
            {
                this.root = left;
            }
            else if(parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;// for deletion
        }

        private Node RotateLeft(Node node)
        {
            Node right = node.Right;
            Node rightLeft = right.Left;
            Node parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if(rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == this.root)
            {
                this.root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }
            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        } 

        private Node RotateLeftRight(Node node)
        {
            Node left = node.Left;
            Node leftRight = left.Right;
            Node parent = node.Parent;
            Node leftRightRight = leftRight.Right;
            Node leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if(leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if(leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }
            
            if(node == this.root)
            {
                this.root = leftRight;
            }
            else if(parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if(leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if(leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;
            return leftRight;

        }

        private Node RotateRightLeft(Node node)
        {
            Node right = node.Right;
            Node rightLeft = right.Left;
            Node parent = node.Parent;
            Node rightLeftLeft = rightLeft.Left;
            Node rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if(node == this.root)
            {
                this.root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        /// <summary>
        /// Deletes a node.
        /// </summary>
        /// <param name="data">The data of the node.</param>
        /// <returns>True if the deletion is successful, false if the node is not in the tree.</returns>
        public bool Delete(int data)
        {
            Node node = this.root;
            while(node != null )
            {
                if(data < node.Data)
                {
                    node = node.Left;
                }
                else if(data > node.Data)
                {
                    node = node.Right;
                }
                else
                {
                    Node left = node.Left;
                    Node right = node.Right;
                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == this.root)
                            {
                                this.root = null;
                            }
                            else
                            {
                                Node parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        Node successor = right;

                        if (successor.Left == null)
                        {
                            Node parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.root)
                            {
                                this.root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            Node parent = node.Parent;
                            Node successorParent = successor.Parent;
                            Node successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.root)
                            {
                                this.root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successorParent, -1);
                        }
                    }
                    this.size--;
                    return true;
                }
            }
            return false;
        }

        private static void Replace(Node target, Node source)
        {
            Node left = source.Left;
            Node right = source.Right;

            target.Balance = source.Balance;
            target.Data = source.Data;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        private void DeleteBalance(Node node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                Node parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        /// <summary>
        /// Checks if the tree contains a node.
        /// </summary>
        /// <param name="data">The node to search for.</param>
        /// <returns>True if the tree contains the node, no otherwise.</returns>
        public bool Contains(int data)
        {
            Node current = this.root;
            while(current != null)
            {
                if(current.Data == data)return true;
                if(current.Data > data)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
            return false;
        }

        /// <summary>
        /// Clears all nodes in the tree.
        /// </summary>
        public void Clear()
        {
            this.root = null;
            this.size = 0;
        }

        /// <summary>
        /// Prints all nodes on the console.
        /// </summary>
        public void DummyPrint()
        {
            DummyPrintNode(this.root);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints all nodes on the console.
        /// </summary>
        public void PrintLinear()
        {
            foreach (int node in this)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }

        private void DummyPrintNode (Node node)
        {
            if(node == null )return;
            Console.WriteLine(node.Data + " l " + (node.Left != null ? node.Left.Data : 0));
            Console.WriteLine(node.Data + " r " + (node.Right != null ? node.Right.Data : 0));
            DummyPrintNode(node.Left);
            DummyPrintNode(node.Right);
        }
        
        public IEnumerator GetEnumerator()
        {
            return new NodeEnumerator(this.root);
        }

        sealed class NodeEnumerator : IEnumerator
        {
            private Node _root;
            private Action _action;
            private Node _current;
            private Node _right;

            public NodeEnumerator(Node root)
            {
                _right = _root = root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public bool MoveNext()
            {
                switch (_action)
                {
                    case Action.Right:
                        _current = _right;

                        while (_current.Left != null)
                        {
                            _current = _current.Left;
                        }

                        _right = _current.Right;
                        _action = _right != null ? Action.Right : Action.Parent;

                        return true;

                    case Action.Parent:
                        while (_current.Parent != null)
                        {
                            Node previous = _current;

                            _current = _current.Parent;

                            if (_current.Left == previous)
                            {
                                _right = _current.Right;
                                _action = _right != null ? Action.Right : Action.Parent;

                                return true;
                            }
                        }

                        _action = Action.End;

                        return false;

                    default:
                        return false;
                }
            }

            public void Reset()
            {
                _right = _root;
                _action = _root == null ? Action.End : Action.Right;
            }

            public int Current
            {
                get
                {
                    return _current.Data;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            enum Action
            {
                Parent,
                Right,
                End
            }
        }
    }
}
