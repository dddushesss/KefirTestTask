using System;

namespace Graph
{
    public class Node
    {
        private NodeView _nodeView;

        public event Action<Node> OnNodeSelected; 

        public Node(NodeView nodeView)
        {
            _nodeView = nodeView;
            _nodeView.OnNodeSelected += () =>
            {
                OnNodeSelected?.Invoke(this);
            };
        }
        
        
        
    }
}