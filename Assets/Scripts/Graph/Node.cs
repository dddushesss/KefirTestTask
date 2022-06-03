using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Graph
{
    public class Node
    {
        private NodeView _nodeView;
        private int _id;
        private int[] _connections;

        public int ID => _id;
        public int Cost => _nodeView.Cost;

        public event Action<Node> OnNodeSelected;

        public int[] Connections => _connections;

        public Node(NodeView nodeView, int id)
        {
            _nodeView = nodeView;
            _id = id;
            _nodeView.OnNodeSelected += () =>
            {
                OnNodeSelected?.Invoke(this);
            };
        }

        public void SetConnections(int[] connections)
        {
            _connections = connections;
        }

        public void SetColor(Color color)
        {
            _nodeView.SetColor(color);
        }
        
    }
}