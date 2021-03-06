using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Graph
{
    [RequireComponent(typeof(Button))]
    public class NodeView : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool isRootNode;
        [SerializeField] private int cost;
        [SerializeField] private NodeView[] connections;

        public IEnumerable<NodeView> Connections => connections;
        public event Action OnNodeSelected;
        public event Action OnDeselected;
        public bool IsRootNode => isRootNode;
        public int Cost => cost;
        public Color Color => button.colors.normalColor;

        private void Start()
        {
            text.text = cost.ToString();
        }

        public void SetColor(Color color)
        {
            var normalColor = button.colors;
            normalColor.normalColor = color;
            button.colors = normalColor;
        }

        public void OnSelect(BaseEventData eventData)
        {
            OnNodeSelected?.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            OnDeselected?.Invoke();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (var connection in connections.Where(node => node != null))
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
#endif
    }
}