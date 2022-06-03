using System.Collections;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class GraphController
    {
        private Node[] _nodes;

        public GraphController(ICollection nodeViews)
        {
            _nodes = new Node[nodeViews.Count];

            for (int i = 0; i < nodeViews.Count; i++)
            {
                _nodes[i] = new Node();
            }
        }
    }
}