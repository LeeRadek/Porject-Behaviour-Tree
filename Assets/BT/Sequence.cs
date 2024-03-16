using Unity.VisualScripting;
using UnityEngine;

namespace BT
{
    public class Sequence : Node
    {
        public Sequence(Node _parent, string name) : base(_parent, name) { }
        
        public override NodeState Evaluate()
        {
            if (parent != null && parent.State == NodeState.Failure)
            {
                return State = NodeState.Failure;
            }

            if (BranchDecorators.Count > 0)
            {
                foreach (var decorator in BranchDecorators)
                {
                    NodeState currentState = decorator("", true);
                    if (currentState == NodeState.Failure)
                    {
                        return State = NodeState.Failure;
                    }
                }
                return NodeState.Success;
            }
            
            if(children.Count == 0)
            {
                return State = NodeState.Failure;
            }

            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                        return State = NodeState.Failure;
                    case NodeState.Running:
                        return State = NodeState.Running;
                    case NodeState.Success:
                        continue;   
                    
                }
            }
            return State = NodeState.Success;
        }
    }
}


