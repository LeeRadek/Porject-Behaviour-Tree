using BT;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ExampleBt : BaseBehaviourTree
{
    private Node root;


    private NavMeshAgent agent;
    public List<Transform> wayPoints;
    public int pointInt = 0;
    public bool canSeePlayer = false;
    public GameObject playerObject;

    private string _name = "Example Tree";
    public override string treeName
    {
        get { return _name; }
        protected set { }
    }

    public override Node _root
    {
        get { return root; }
        protected set { }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetupTree();
    }

    void Start()
    {




    }

    void Update()
    {
        //root.Evaluate();
        Execute();
    }

    void SetupTree()
    {
        root = new Selector(null, "root");
        Selector SL = new Selector(root, "SL");
        Sequence sq = new Sequence(root, "sq");
        Task task1 = new Task(ExampleMetod, sq, "Example Method");
        Task task2 = new Task(Move, sq, "Move");
        Task SeePlayer = new Task(CanSeePlayer, SL, "Can See Player");
        Selector q = new Selector(sq, "SQ");

        Selector s = new Selector(SL, "S");
        Sequence sequence = new Sequence(s, "sequence");
        Task T = new Task(qwe, s, "T");
        s.AddDecorator(true, "Decorator Name");

        root.AddChild(s);
        s.AddChild(task1, T, sequence);

        //root.AddChild(sq, SL);
        sq.AddChild(task2);
        SL.AddChild(SeePlayer);


        //BTDebugger debugger = gameObject.AddComponent<BTDebugger>();
        //debugger.DebugTree(root);
    }

    NodeState ExampleMetod()
    {
        Debug.Log("Example");
        return NodeState.Success;
    }

    NodeState Move()
    {
        if (canSeePlayer)
        {
            return NodeState.Failure;
        }
        if (agent.remainingDistance < .1f || !agent.hasPath)
        {
            agent.SetDestination(wayPoints[pointInt].position);
            pointInt++;
            if (pointInt == 4)
            {
                pointInt = 0;
            }
        }

        return NodeState.Success;
    }

    NodeState CanSeePlayer()
    {
        if (canSeePlayer)
        {

            agent.SetDestination(playerObject.transform.position);
            return NodeState.Success;
        }
        else
        {

            return NodeState.Failure;
        }
    }

    NodeState qwe()
    {
        Debug.LogWarning("Task T");
        return NodeState.Success;
    }





    public override void Execute()
    {
        root.Evaluate();
    }


}
