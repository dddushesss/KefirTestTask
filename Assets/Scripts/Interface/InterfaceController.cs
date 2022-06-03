using System;
using Graph;
using TMPro;

namespace Interface
{
    public class InterfaceController
    {
        private GraphController _graphController;
        private InterfaceView _interfaceView;
        public event Action<TMP_Text> OnAddButtonClicked;

        public InterfaceController(GraphController graphController, InterfaceView interfaceView)
        {
            _graphController = graphController;
            _interfaceView = interfaceView;
            
        }

        public void SubscribeStudyButtons()
        {
            _interfaceView.OnStudyButtonClicked += _graphController.StudyNode;
            _interfaceView.OnForgetButtonClicked += _graphController.ForgetNode;
            _interfaceView.OnForgetAllButtonClicked += _graphController.ForgetAllNodes;
            _interfaceView.OnPointsAddButtonClicked += _graphController.AddPoint;
            _graphController.OnPointCountChaged += ChangePointCount;
        }

        private void ChangePointCount(int points)
        {
            _interfaceView.SetPoints(points);
        }
    }
}