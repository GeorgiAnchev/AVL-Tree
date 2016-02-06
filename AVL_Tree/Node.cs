using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public partial class Tree
    {
        private class Node
        {
            public int Data;
            public Node Left;
            public Node Right;
            public Node Parent;
            public int Balance;
            public Node(int data, Node parent = null)
            {
                this.Data = data;
                this.Parent = parent;
            }
        }
    }

}
