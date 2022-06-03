using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    public class InterfaceView : MonoBehaviour
    {
        [SerializeField] private Button studyButton;
        [SerializeField] private Button forgetButton;
        [SerializeField] private Button forgetAllButton;
        [SerializeField] private Button addPointButton;
        [SerializeField] private TMP_Text poitsText;

        public event Action OnStudyButtonClicked;
        public event Action OnForgetButtonClicked; 
        public event Action OnForgetAllButtonClicked;
        public event Action OnPointsAddButtonClicked; 
        private void Start()
        {
            studyButton.onClick.AddListener(() => OnStudyButtonClicked?.Invoke());
            forgetButton.onClick.AddListener(() => OnForgetButtonClicked?.Invoke());
            forgetAllButton.onClick.AddListener(() => OnForgetAllButtonClicked?.Invoke());
            addPointButton.onClick.AddListener(() => OnPointsAddButtonClicked?.Invoke());
        }

        public void SetPoints(int points)
        {
            poitsText.text = points.ToString();
        }

        public void SetStudyButtonAvailable(bool isAvailable)
        {
            studyButton.interactable = isAvailable;
        }
        
        public void SetForgetButtonAvailable(bool isAvailable)
        {
            forgetButton.interactable = isAvailable;
        }
        
        public void SetForgetAllButtonAvailable(bool isAvailable)
        {
            forgetAllButton.interactable = isAvailable;
        }
        
    }
}