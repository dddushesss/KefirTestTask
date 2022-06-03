using System;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    public class InterfaceView : MonoBehaviour
    {
        [SerializeField] private Button studyButton;
        [SerializeField] private Button forgetButton;
        [SerializeField] private Button forgetAllButton;

        public event Action OnStudyButtonClicked;
        public event Action OnForgetButtoClicked; 
        public event Action OnForgetAllButtoClicked; 
        private void Start()
        {
            studyButton.onClick.AddListener(() => OnStudyButtonClicked?.Invoke());
            forgetButton.onClick.AddListener(() => OnForgetButtoClicked?.Invoke());
            forgetAllButton.onClick.AddListener(() => OnForgetAllButtoClicked?.Invoke());
        }
    }
}