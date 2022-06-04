using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Graph
{
    public class Node
    {
        private NodeView _nodeView;
        public bool isOpened;

        public int ID { get; }

        public int Cost => _nodeView.Cost;
        public Color Color => _nodeView.Color;

        public event Action<Node> OnNodeSelected;

        public List<Node> Connections { get; }

        public Node(NodeView nodeView, int id)
        {
            Connections = new List<Node>();
            isOpened = false;
            _nodeView = nodeView;
            ID = id;
            _nodeView.OnNodeSelected += () => { OnNodeSelected?.Invoke(this); };
        }

        public void AddConnections(Node connectedNode)
        {
            Connections.Add(connectedNode);
        }

        public void SetColor(Color color)
        {
            _nodeView.SetColor(color);
        }

        public bool HasConnectionToRoot(Node node)
        {
            List<Node> closedList = new List<Node>() { node };
            Stack<Node> stack = new Stack<Node>();

            var curNode = this;
            stack.Push(curNode);
            do
            {
                if (curNode._nodeView.IsRootNode || curNode.Connections.Any(n => n._nodeView.IsRootNode))
                    return true;
                closedList.Add(curNode);
                var openList = curNode.Connections.Where(n => n.isOpened && !closedList.Contains(n)).ToList();
                if (openList.Count == 0)
                {
                    if (stack.Any())
                        curNode = stack.Pop();
                    continue;
                }

                curNode = openList.PopRandom();

                stack.Push(curNode);
            } while (stack.Any());

            return false;
        }
    }
}