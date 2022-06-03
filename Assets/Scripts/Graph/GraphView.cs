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

        private void Start()
        {
            GraphController controller = new GraphController(nodes, lineRendererPrefab);
            InterfaceController interfaceController = new InterfaceController();
            
        }

        [EditorButton]
        private void AddNode()
        {
            var nodeView = Instantiate(nodePrefab, transform);
            nodes.Add(nodeView);
        }
    }
}