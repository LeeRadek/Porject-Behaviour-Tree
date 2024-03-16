using System.Text;
using UnityEngine;

namespace BT
{
    public class BTDebugger : MonoBehaviour
    {
        private StringBuilder treeHierarchy = new();
        public void DebugTree(Node root)
        {
            DebugNode(root, 0);
            Debug.Log(treeHierarchy.ToString());

        }
        private void DebugNode(Node node, int depth)
        {
            string ident = new string('-', depth * 2);
            treeHierarchy.AppendLine($"{ident} {node.GetType().Name} {node.nodeName} {node.Evaluate()}");
            foreach (Node child in node.children)
            {
                DebugNode(child, depth + 1);
            }
        }
    }
}


