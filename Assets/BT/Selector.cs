namespace BT
{
    public class Selector : Node
    {
        public Selector(Node _parent, string name) : base(_parent, name) { }

        public override NodeState Evaluate()
        {
            if(parent != null)
            {
                if(parent.State == NodeState.Failure)
                {
                    return State = NodeState.Failure;
                }
            }

            if(children.Count == 0)
            {
                return State = NodeState.Failure;
            }
            
            foreach (var decorator in BranchDecorators)
            {
                NodeState currentState = decorator("", true);
                if (currentState == NodeState.Failure)
                {
                    foreach (Node child in children)
                    {
                        return child.State = NodeState.Failure;
                    }
                }
                return State = NodeState.Success;
            }
            
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Success:
                        return State = NodeState.Success;
                    case NodeState.Running:
                        return State = NodeState.Running;
                    case  NodeState.Failure:
                        continue;
                }
            }
            return State = NodeState.Failure;
        }
    }
}


