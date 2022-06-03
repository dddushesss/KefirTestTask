using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Graph
{
    [RequireComponent(typeof(Button))]
    public class NodeView : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private bool isRootNode;
        [SerializeField] private int cost;
        [SerializeField] private NodeView[] connections;
        private int _id;

        public NodeView[] Connections => connections;
        public event Action OnNodeSelected;
        public event Action OnDeselected;
        public bool IsRootNode => isRootNode;
        public int Cost => cost;

        private void Start()
        {
            _text.text = cost.ToString();
        }

        public void SetColor(Color color)
        {
            var normalColor = _button.colors;
            normalColor.normalColor = color;
            _button.colors = normalColor;
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
            foreach (var connection in connections)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
#endif
       
    }
}
