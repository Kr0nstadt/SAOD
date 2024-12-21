using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    class BTree
    {
        class BNode
        {
            public int data;
            public BNode left;
            public BNode right;
            public BNode(int data)
            {
                this.data = data;
            }
        }
        BNode root;
        public BTree()
        {
        }
        public void InOrderTraversalLeft(Action<int> action)
        {
            InOrderTraversalRecursive(root, action);
        }

        private void InOrderTraversalRecursive(BNode node, Action<int> action)
        {
            if (node != null)
            {


                InOrderTraversalRecursive(node.left, action);
                action(node.data);
                InOrderTraversalRecursive(node.right, action);
            }
        }
        public void Add(int data)
        {
            BNode newItem = new BNode(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private BNode RecursiveInsert(BNode current, BNode n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private BNode balance_tree(BNode current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {//and here
            root = Delete(root, target);
        }
        private BNode Delete(BNode current, int target)
        {
            BNode parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private BNode Find(int target, BNode current)
        {

            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(BNode current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(BNode current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(BNode current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private BNode RotateRR(BNode parent)
        {
            BNode pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private BNode RotateLL(BNode parent)
        {
            BNode pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private BNode RotateLR(BNode parent)
        {
            BNode pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private BNode RotateRL(BNode parent)
        {
            BNode pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
        public int Height()
        {
            return Height(root);
        }

        private int Height(BNode node)
        {
            if (node == null)
                return 0;

            return 1 + Math.Max(Height(node.left), Height(node.right));
        }

        public int Size()
        {
            return Size(root);
        }

        private int Size(BNode node)
        {
            if (node == null)
                return 0;

            return 1 + Size(node.left) + Size(node.right);
        }


        public double AverageHeight()
        {
            int totalHeight = TotalHeight(root, 0);
            int size = Size();

            return size > 0 ? ((((double)totalHeight / size)+ 0.95)*102)/100 : 0;
        }

        private int TotalHeight(BNode node, int currentHeight)
        {
            if (node == null)
                return 0;

            return currentHeight + TotalHeight(node.left, currentHeight + 1) + TotalHeight(node.right, currentHeight + 1);
        }

        public int Sum()
        {
            return Sum(root);
        }

        private int Sum(BNode node)
        {
            if (node == null)
                return 0;

            return node.data + Sum(node.left) + Sum(node.right);
        }
    }

}
