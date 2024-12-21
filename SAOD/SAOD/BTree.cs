using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SAOD
{
    public class Dbds
    {
        public Element[] Keys;
        public Dbds[] Children;
        public int NumKeys;
        public bool Leaf;

        public Dbds(Element key)
        {
            Keys = new Element[1];
            Keys[0] = key;
            NumKeys = 1;
            Leaf = true;
            Children = null;
        }
    }

    public class Tree
    {
        public void InsertDbd(Element element, ref Dbds p)
        {
            if (p == null)
            {
                p = new Dbds(element); 
                return;
            }

            if (p.Leaf)
            {
                if (p.NumKeys < 1)
                {
                    p.Keys[0] = element;
                    p.NumKeys++;
                }
                else
                {
                    Element oldKey = p.Keys[0];
                    if (element.NamApart < oldKey.NamApart)
                    {
                        p.Keys[0] = element;
                        Dbds newChild = new Dbds(oldKey);
                        newChild.Leaf = true;
                        p.Children = new Dbds[2]; 
                        p.Children[0] = null;      
                        p.Children[1] = newChild;  
                    }
                    else
                    {
                        Dbds newChild = new Dbds(element);
                        p.Children = new Dbds[2];
                        p.Children[0] = newChild; 
                        p.Children[1] = null;    
                    }
                }
            }
            else
            {
                if (element.NamApart < p.Keys[0].NamApart)
                {
                    InsertDbd(element, ref p.Children[0]);
                }
                else
                {
                    InsertDbd(element, ref p.Children[1]);
                }
            }
        }

        public void InOrderTraversal(Dbds node)
        {
            if (node == null) return;

            if (node.Leaf)
            {
                Console.Write(node.Keys[0] + " ");
            }
            else
            {
                InOrderTraversal(node.Children[0]);
                Console.Write(node.Keys[0] + " "); 
                InOrderTraversal(node.Children[1]);
            }
        }
    }
}

