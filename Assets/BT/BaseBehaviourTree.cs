using BT;
using UnityEngine;

public abstract class BaseBehaviourTree : MonoBehaviour
{
    public abstract Node _root { get; protected set; }
    public abstract string treeName { get; protected set; }
    public abstract void Execute();
}
