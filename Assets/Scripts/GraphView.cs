using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    [SerializeField] private List<NodeView> nodes;

    [SerializeField] private NodeView NodePrefab;

    private void Start()
    {
        GraphController controller = new GraphController(nodes);
    }

    [EditorButton]
    private void AddNode()
    {
        var nodeView = Instantiate(NodePrefab);
        nodes.Add(nodeView);
    }
}