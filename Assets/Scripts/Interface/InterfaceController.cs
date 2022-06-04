using Graph;

namespace Interface
{
    public class InterfaceController
    {
        private GraphController _graphController;
        private InterfaceView _interfaceView;

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
            _interfaceView.OnPointsAddButtonClicked += () => _graphController.AddPoint(1);
            _graphController.OnPointCountChanged += ChangePointCount;
            _graphController.OnNodeSelectedCanStudy += _interfaceView.SetStudyButtonAvailable;
            _graphController.OnNodeSelectedCanForget += _interfaceView.SetForgetButtonAvailable;
        }

        private void ChangePointCount(int points)
        {
            _interfaceView.SetPoints(points);
        }
        
    }
}