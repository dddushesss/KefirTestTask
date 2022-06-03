using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NodeView : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    private Node _node;

    public event Action<Node> OnNodeSelected; 
    private void Start()
    {
      
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnNodeSelected?.Invoke(_node);
    }
}
