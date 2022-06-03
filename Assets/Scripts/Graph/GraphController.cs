using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Graph
{
    public class GraphController
    {
        private Node[] _nodes;
        private Node _rootNode;
        private Node _selectedNode;
        private int _points;
        public event Action<int> OnPointCountChaged; 

        public GraphController(IReadOnlyList<NodeView> nodeViews, LineRenderer lineRendererPrefab)
        {
            _nodes = new Node[nodeViews.Count];
            for (int i = 0; i < nodeViews.Count; i++)
            {
                _nodes[i] = new Node(nodeViews[i]);
                
                foreach (var connection in nodeViews[i].Connections)
                {
                    var line = Object.Instantiate(lineRendererPrefab);
                    line.SetPositions(new []{nodeViews[i].transform.position, connection.transform.position});
                }
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
                nodeViews[i].OnDeselected += () => { _selectedNode = null; };
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

        public void AddPoint()
        {
            _points++;
            OnPointCountChaged?.Invoke(_points);
        }

        public void SetPoint(int points)
        {
            _points = points;
            OnPointCountChaged?.Invoke(_points);
        }

        private void SelectNode(Node node)
        {
            _selectedNode = node;
        }
    }
}