using BT;
using Codice.Client.BaseCommands;
using log4net.Appender;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BTDebugerWindow : EditorWindow
{
    private BaseBehaviourTree behaviourTree;
    private GameObject selectedObject;

    private List<string> sucessfulNodes = new();

    private bool showDebugBT = false;
    private bool showSucessNodes = false;

    private GUIStyle normalText;
    private GUIStyle greenText;
    private GUIStyle redText;


    [MenuItem("Tools/Tree Debuger")]
    public static void ShowWindow()
    {
        GetWindow<BTDebugerWindow>("Behaviour Tree Debuger");
    }

    private void OnEnable()
    {

    }

    private void OnGUI()
    {
        selectedObject = Selection.activeGameObject;

        normalText = new GUIStyle(GUI.skin.label);
        greenText = new GUIStyle(GUI.skin.label);
        redText = new GUIStyle(GUI.skin.label);

        greenText.normal.textColor = Color.green;
        redText.normal.textColor = Color.red;
        normalText.normal.textColor = Color.white;

        showDebugBT = GUILayout.Toggle(showDebugBT, "Show BT Debuger");
        showSucessNodes = GUILayout.Toggle(showSucessNodes, "Show Sucesses Nodes");

        if (selectedObject != null)
        {
            behaviourTree = selectedObject.GetComponent<BaseBehaviourTree>();
            GUILayout.Label(behaviourTree.treeName);

            if (behaviourTree != null && showDebugBT)
            {
                Node n = behaviourTree._root;
                DebugNode(behaviourTree._root, 0);



            }


            Repaint();
        }
        else
        {
            EditorGUILayout.HelpBox("Select GameObject in Hierarchy with Behaviour Tree", MessageType.Warning);
        }

        if (showSucessNodes)
        {
            DisplaySucessfulyNodes();
        }




    }



    private void DebugNode(Node node, int depth)
    {

        if (node == null)
        {
            return;
        }

        string hierarchy = node != null ? node.GetType()?.Name + ": " + node.nodeName : null;
        string evaluate = node != null ? node.Evaluate().ToString() : null;
        string ident = new string('-', depth * 1);



        hierarchy = ident + hierarchy;


        float width1 = GUI.skin.label.CalcSize(new GUIContent(hierarchy)).x;
        float width2 = GUI.skin.label.CalcSize(new GUIContent(evaluate)).x;
        float spaceing = Mathf.Max(width1, width2);

        GUILayout.BeginHorizontal();
        GUILayout.Label(hierarchy, normalText);
        GUILayout.Space(Mathf.Clamp(spaceing * -1, -175, -275));

        if (node.Evaluate() == NodeState.Success)
        {
            GUILayout.Label(evaluate, greenText);
            if (!sucessfulNodes.Contains(hierarchy))
            {
                sucessfulNodes.Add(hierarchy);
                Repaint();
            }
        }

        if (node.Evaluate() == NodeState.Failure)
        {
            GUILayout.Label(evaluate, redText);
            if (!sucessfulNodes.Contains(hierarchy))
            {
                sucessfulNodes.Remove(hierarchy);
                Repaint();
            }
        }

        GUILayout.EndHorizontal();





        hierarchy = ident + hierarchy;

        foreach (Node child in node.children)
        {
            DebugNode(child, depth + 1);
        }

    }

    private void DisplaySucessfulyNodes()
    {
        foreach (string node in sucessfulNodes)
        {
            GUILayout.Label(node, normalText);
            Debug.Log(node);
        }
    }



}


