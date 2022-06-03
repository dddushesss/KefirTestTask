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
        private List<Node> _studiedNodes;
        private List<Node> _closedNodes;
        private List<Node> _availableToStudyNodes;
        private Color _studiedColor;
        public event Action<int> OnPointCountChanged;
        public event Action<bool> OnNodeSelectedCanStudy;
        public event Action<bool> OnNodeSelectedCanForget;

        public GraphController(IReadOnlyDictionary<NodeView, int> nodeViews, LineRenderer lineRendererPrefab,
            Color openedColor)
        {
            _nodes = new Node[nodeViews.Count];
            _studiedNodes = new List<Node>(nodeViews.Count);
            _closedNodes = new List<Node>(nodeViews.Count);
            _availableToStudyNodes = new List<Node>(nodeViews.Count);

            _studiedColor = openedColor;
            foreach (var node in nodeViews)
            {
                _nodes[node.Value] = new Node(node.Key, node.Value);
                var connections = new int[node.Key.Connections.Length];
                var i = 0;
                foreach (var connection in node.Key.Connections)
                {
                    var line = Object.Instantiate(lineRendererPrefab);
                    line.SetPositions(new[] { node.Key.transform.position, connection.transform.position });
                    connections[i++] = nodeViews[connection];
                }

                _nodes[node.Value].SetConnections(connections);
                if (node.Key.IsRootNode)
                {
                    if (_rootNode != null)
                    {
                        Debug.LogError("Более 1 корневого узла");
                        return;
                    }

                    _rootNode = _nodes[node.Value];
                    _rootNode.SetColor(openedColor);
                    _studiedNodes.Add(_rootNode);
                }
                else
                {
                    _closedNodes.Add(_nodes[node.Value]);
                }

                _nodes[node.Value].OnNodeSelected += SelectNode;
                node.Key.OnDeselected += () =>
                {
                    _selectedNode = null;
                    OnNodeSelectedCanStudy?.Invoke(false);
                };
            }

            foreach (var nodeId in _rootNode?.Connections)
            {
                _availableToStudyNodes.Add(_nodes[nodeId]);
            }
        }

        public void StudyNode()
        {
            _availableToStudyNodes.Remove(_selectedNode);
            foreach (var id in _selectedNode.Connections)
            {
                _availableToStudyNodes.Add(_nodes[id]);
            }
            _studiedNodes.Add(_selectedNode);
            _selectedNode.SetColor(_studiedColor);
            AddPoint(-_selectedNode.Cost); 
        }

        public void ForgetNode()
        {
        }

        public void ForgetAllNodes()
        {
            var selected = _selectedNode;
            foreach (var node in _studiedNodes)
            {
                _selectedNode = node;
                ForgetNode();
            }

            _selectedNode = selected;
        }

        public void AddPoint(int points)
        {
            _points+= points;
            OnPointCountChanged?.Invoke(_points);
        }

        public void SetPoint(int points)
        {
            _points = points;
            OnPointCountChanged?.Invoke(_points);
            OnNodeSelectedCanStudy?.Invoke(false);
            OnNodeSelectedCanForget?.Invoke(false);
        }

        private void SelectNode(Node node)
        {
            _selectedNode = node;
            OnNodeSelectedCanStudy?.Invoke(_availableToStudyNodes.Contains(node));
            OnNodeSelectedCanForget?.Invoke(_studiedNodes.Contains(node) && node.ID != _rootNode.ID);
        }
    }
}