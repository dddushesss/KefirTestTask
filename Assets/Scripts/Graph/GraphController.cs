using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Singleton;
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
        private Color _defaultColor;
        public event Action<int> OnPointCountChanged;
        public event Action<bool> OnNodeSelectedCanStudy;
        public event Action<bool> OnNodeSelectedCanForget;

        public GraphController(IReadOnlyDictionary<NodeView, int> nodeViews, LineRenderer lineRendererPrefab,
            Color openedColor)
        {
            Singleton<TimerHelper>.Init("TimerHelper");

            _nodes = new Node[nodeViews.Count];
            _studiedNodes = new List<Node>(nodeViews.Count);
            _closedNodes = new List<Node>(nodeViews.Count);
            _availableToStudyNodes = new List<Node>(nodeViews.Count);
            _studiedColor = openedColor;
            var connectionList = new List<(int id1, int id2)>();

            foreach (var node in nodeViews)
            {
                _nodes[node.Value] = new Node(node.Key, node.Value);

                foreach (var connection in node.Key.Connections)
                {
                    var line = Object.Instantiate(lineRendererPrefab, connection.transform);
                    line.SetPositions(new[] { node.Key.transform.position, connection.transform.position });
                    connectionList.Add((node.Value, nodeViews[connection]));
                }

                if (node.Key.IsRootNode)
                {
                    if (_rootNode != null)
                    {
                        Debug.LogError("Более 1 корневого узла");
                        return;
                    }

                    _rootNode = _nodes[node.Value];
                    _defaultColor = _rootNode.Color;
                    _rootNode.SetColor(openedColor);
                    _rootNode.isOpened = true;
                    _studiedNodes.Add(_rootNode);
                }
                else
                {
                    _closedNodes.Add(_nodes[node.Value]);
                }

                _nodes[node.Value].OnNodeSelected += SelectNode;
                node.Key.OnDeselected += () =>
                {
                    Singleton<TimerHelper>.Instance.StartTimer(() =>
                    {
                        _selectedNode = null;
                        OnNodeSelectedCanStudy?.Invoke(false);
                    }, 0.1f);
                };
            }

            if (_rootNode == null)
            {
                Debug.LogError("Не найдено ни одного корневого узла");
                return;
            }

            foreach (var connection in connectionList)
            {
                _nodes[connection.id1].AddConnections(_nodes[connection.id2]);
                _nodes[connection.id2].AddConnections(_nodes[connection.id1]);
            }

            foreach (var node in _rootNode?.Connections)
            {
                _availableToStudyNodes.Add(node);
            }
        }


        public void StudyNode()
        {
            _availableToStudyNodes.Remove(_selectedNode);
            foreach (var node in _selectedNode.Connections.Where(node => !node.isOpened))
            {
                _availableToStudyNodes.Add(node);
            }

            _selectedNode.isOpened = true;
            _studiedNodes.Add(_selectedNode);
            _selectedNode.SetColor(_studiedColor);
            AddPoint(-_selectedNode.Cost);
        }

        public void ForgetNode()
        {
            _selectedNode.SetColor(_defaultColor);
            _studiedNodes.Remove(_selectedNode);
            AddPoint(_selectedNode.Cost);
            foreach (var node1 in _selectedNode.Connections.Where(node => !node.isOpened))
            {
                _availableToStudyNodes.Remove(node1);
            }

            _availableToStudyNodes.Add(_selectedNode);
            OnNodeSelectedCanForget?.Invoke(false);
        }

        public void ForgetAllNodes()
        {
            foreach (var node in _studiedNodes.Where(n => n.ID != _rootNode.ID).ToList())
            {
                _selectedNode = node;
                ForgetNode();
            }

            _selectedNode = null;
        }

        public void AddPoint(int points)
        {
            _points += points;
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
            Singleton<TimerHelper>.Instance.StartTimer(() =>
            {
                _selectedNode = node;
                OnNodeSelectedCanStudy?.Invoke(_availableToStudyNodes.Contains(node)
                                               && _points - _selectedNode.Cost >= 0);
                OnNodeSelectedCanForget?.Invoke(_studiedNodes.Contains(node)
                                                && node.ID != _rootNode.ID
                                                && (_selectedNode.Connections
                                                    .Where(n => n.isOpened).ToList()
                                                    .TrueForAll(n => n.HasConnectionToRoot(node))));
            }, 0.2f);
        }
    }
}