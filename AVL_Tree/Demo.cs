using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL_Tree;

namespace AVL_TreeDemo
{
    class Demo
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();

            while (true)
            {
                string input = Console.ReadLine();
                char[] delimiterChars = { ' ' };
                var words = input.Split(delimiterChars);
                if (words[0] == "insert")
                {
                    int number = int.Parse(words[1]);
                    Console.WriteLine(tree.Insert(number));
                }
                else if (words[0] == "delete")
                {
                    int number = int.Parse(words[1]);
                    Console.WriteLine(tree.Delete(number));
                }
                else if (words[0] == "contains")
                {
                    int number = int.Parse(words[1]);
                    Console.WriteLine(tree.Contains(number));
                }
                else if(words[0] == "size")
                {
                    Console.WriteLine(tree.Size);
                }
                else if(words[0] == "print")
                {
                    tree.PrintLinear();
                }
                else if (words[0] == "break") break;
            }

            tree.Insert(8);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(5);
            tree.DummyPrint();
            //tree.RotateRight(tree.root);
            //tree.print();

            tree.Insert(5);
            tree.Insert(12);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(64);
            tree.Insert(35);
            tree.Insert(9);
            tree.Insert(2);

            Console.WriteLine("Contains 7: " + tree.Contains(7));
            Console.WriteLine("Contains 42: " + tree.Contains(42));

            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(23);
            tree.Insert(73);
            tree.Insert(57);
            tree.Insert(87);
            tree.Insert(17);
            tree.Insert(17);
            tree.Insert(43);
            tree.Insert(6);
            tree.Insert(5);
            tree.DummyPrint();

            tree.Delete(57);
            tree.Delete(87);
            tree.Delete(2);
            bool succesful1 = tree.Delete(100);
            bool succesful2 = tree.Delete(12);
            tree.Delete(17);
            tree.DummyPrint();

            foreach(int node in tree)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();

            
        }
    }
}
