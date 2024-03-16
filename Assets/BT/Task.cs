using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine.Analytics;


namespace BT
{
    public class Task : Node
    {
        private readonly Func<NodeState> func;
        
        public Task(Func<NodeState> _func, Node _parent, string name) : base(_parent, name)
        {
            func = _func;

        }



        public override NodeState Evaluate()
        {
            if(parent != null)
            {
                if(parent.State == NodeState.Failure)
                {
                    return NodeState.Failure;
                }
                else if (parent.State == NodeState.Success && func != null)
                {
                    //NodeState result = func.Invoke();
                    //State = func.Invoke();
                    return func.Invoke();
                }
            }


            return NodeState.Failure;

        }

    }
}


