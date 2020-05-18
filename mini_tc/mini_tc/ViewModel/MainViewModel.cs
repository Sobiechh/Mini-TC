using System;

namespace mini_tc.ViewModel
{
    using Base;
    using System.Windows.Input;

    class MainViewModel : BaseViewModel
    {
        #region Prop-s
        private SideViewModel _leftPanel;
        public SideViewModel LeftPanel
        {
            get { return _leftPanel; }
            set { _leftPanel = value; OnPropertyChanged(nameof(LeftPanel)); }
        }

        private SideViewModel _rightPanel;
        public SideViewModel RightPanel
        {
            get { return _rightPanel; }
            set { _rightPanel = value; OnPropertyChanged(nameof(RightPanel)); }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            _leftPanel = new SideViewModel();
            _rightPanel = new SideViewModel();

            Copy = new RelayCommand(CopyExecute, CopyCanExecute);
            LeftSelectionChange = new RelayCommand(LeftSelectionChangeExecute, arg => true);
            RightSelectionChange = new RelayCommand(RightSelectionChangeExecute, arg => true);
        }
        #endregion

        #region Commands
        public ICommand Copy { get; set; }
        public ICommand LeftSelectionChange { get; set; }
        public ICommand RightSelectionChange { get; set; }

        //tests
        private void CopyExecute(object obj)
        {
            //Console.WriteLine("From : " + LeftPanel.SelectedPath);
            //Console.WriteLine("Where : " + RightPanel.SelectedPath);
        }

        private bool CopyCanExecute(object obj)
        {
            return LeftPanel.SelectedPath != null || RightPanel.SelectedPath != null;
        }

        private void LeftSelectionChangeExecute(object obj)
        {
            if (RightPanel.SelectedPath != null && LeftPanel.SelectedPath != null)
                RightPanel.SelectedPath = null;
        }

        private void RightSelectionChangeExecute(object obj)
        {
            if (LeftPanel.SelectedPath != null && RightPanel.SelectedPath != null)
                LeftPanel.SelectedPath = null;
        }
        #endregion
    }
}