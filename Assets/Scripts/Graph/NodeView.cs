using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Graph
{
    [RequireComponent(typeof(Button))]
    public class NodeView : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color openedColor;
        [SerializeField] private bool isRootNode;
        [SerializeField] private int cost;
        [SerializeField] private NodeView[] connections;
      
        public event Action OnNodeSelected;

        public bool IsRootNode => isRootNode;

        public int Cost => cost;

        private void Start()
        {
            _text.text = cost.ToString();
        }

        public void OnSelect(BaseEventData eventData)
        {
            OnNodeSelected?.Invoke();
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
