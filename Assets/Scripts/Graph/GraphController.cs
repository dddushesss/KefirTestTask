using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
    public class GraphController
    {
        private Node[] _nodes;
        private Node _rootNode;
        private Node _selectedNode;

        public GraphController(IReadOnlyList<NodeView> nodeViews, LineRenderer lineRendererPrefab)
        {
            _nodes = new Node[nodeViews.Count];

            for (int i = 0; i < nodeViews.Count; i++)
            {
                _nodes[i] = new Node(nodeViews[i]);
                if (nodeViews[i].IsRootNode)
                {
                    if (_rootNode != null)
                    {
                        Debug.LogError("Более 1 корневого узла");
                        return;
                    }

                    _rootNode = _nodes[i];
                }

                _nodes[i].OnNodeSelected += SelectNode;
            }
        }

        public void StudyNode()
        {
        }

        public void ForgetNode()
        {
        }

        public void ForgetAllNodes()
        {
            
        }

        private void SelectNode(Node node)
        {
            _selectedNode = node;
        }
    }
}