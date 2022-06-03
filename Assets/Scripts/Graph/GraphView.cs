using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Graph
{
    public class GraphView : MonoBehaviour
    {
        [SerializeField] private List<NodeView> nodes;
        [SerializeField] private NodeView nodePrefab;
        [SerializeField] private LineRenderer lineRendererPrefab;
        [SerializeField] private InterfaceView interfaceView;
        [SerializeField] private Color openedColor;
        [SerializeField] private int startPoints;
        private Dictionary<NodeView, int> nodesId;

        private void Start()
        {
            nodesId = new Dictionary<NodeView, int>(nodes.Count);
            for (var i = 0; i < nodes.Count; i++)
            {
                nodesId.Add(nodes[i], i);
            }
            GraphController controller = new GraphController(nodesId, lineRendererPrefab, openedColor);
            InterfaceController interfaceController = new InterfaceController(controller, interfaceView);
            interfaceController.SubscribeStudyButtons();
            controller.SetPoint(startPoints);
        }

        [EditorButton]
        private void AddNode()
        {
            var nodeView = Instantiate(nodePrefab, transform);
            nodes.Add(nodeView);
        }
    }
}