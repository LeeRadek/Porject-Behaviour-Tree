using System.Collections.Generic;
using UnityEngine;
namespace BT
{
    using System;

    public enum NodeState
    {
        Success,
        Running,
        Failure
    }

    public abstract class Node
    {
        protected Node parent;
        public string nodeName;
        public NodeState State;
        public List<Node> children = new List<Node>();
        public List<Func<string, bool, NodeState>> BranchDecorators = new List<Func<string, bool, NodeState>>();


        public Node(Node _parent, string name)
        {
            parent = _parent;
            nodeName = name;
        }

        public void AddChild(params Node[] nodes)
        {
            foreach (Node child in nodes)
            {
                children.Add(child);
            }
        }

        public void AddDecorator(bool condition, string name)
        {
            BranchDecorators.Add((name ,boolValue) => condition ? NodeState.Success : NodeState.Failure);
        }

        public abstract NodeState Evaluate();

    }
}

