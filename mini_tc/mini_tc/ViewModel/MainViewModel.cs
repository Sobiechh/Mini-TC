//using Base;
//czemu nie dziala tutaj?

namespace mini_tc.ViewModel
{
    using System.IO;
    using mini_tc.Properties;
    using System.Windows.Input;
    using Base;
    using mini_tc.Model;
    // Jeżeli Pan Doktor to widzi to proszę o wytłumaczenie czemu using tutaj

    class MainViewModel : BaseViewModel
    {
        #region Props
        //copy model 
        private CopyModel copyModel;


        private SideViewModel _leftSide;
        public SideViewModel LeftSide
        {
            get { return _leftSide; }
            set { _leftSide = value; OnPropertyChanged(nameof(LeftSide)); }
        }

        private SideViewModel _rightSide;
        public SideViewModel RightSide
        {
            get { return _rightSide; }
            set { _rightSide = value; OnPropertyChanged(nameof(RightSide)); }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            _leftSide = new SideViewModel();
            _rightSide = new SideViewModel();

            copyModel = new CopyModel();

            Copy = new RelayCommand(CopyExecute, CopyCanExecute);
            LeftSelectionChange = new RelayCommand(LeftSelectionChangeExecute, arg => true);
            RightSelectionChange = new RelayCommand(RightSelectionChangeExecute, arg => true);
        }
        #endregion

        #region Commands
        //Icommands
        public ICommand Copy { get; set; }
        public ICommand LeftSelectionChange { get; set; }
        public ICommand RightSelectionChange { get; set; }

        //tests
        //copy function in models
        private void CopyExecute(object obj)
        {
            //tests
            //Console.WriteLine("From : " + LeftSide.SelectedPath);
            //Console.WriteLine("Where : " + RightSide.SelectedPath)

            string source = "";
            string target = "";
            //LEFT SIDE
            if (LeftSide.SelectedPath != null)
            {
                source = Path.Combine(LeftSide.CurrentPath, LeftSide.GetSelectedPath());
                target = Path.GetFullPath(RightSide.CurrentPath);
            } // RIGHT SIDE
            else if (RightSide.SelectedPath != null)
            {
                source = Path.Combine(RightSide.CurrentPath, RightSide.GetSelectedPath());
                target = Path.GetFullPath(LeftSide.CurrentPath);
            }


            copyModel.Copy(source, target); // Model -> CopyModel.cs 

            UpdateView(); //UpdateView 
        }

        private bool CopyCanExecute(object obj)
        {
            // check if previous clicked
            if (LeftSide.SelectedPath == null && RightSide.SelectedPath == null) return false;
            if (LeftSide.SelectedPath == Resources.PreviousDirectory || RightSide.SelectedPath == Resources.PreviousDirectory) return false;
            return true;
        }

        //left select
        private void LeftSelectionChangeExecute(object obj)
        {
            //(RightSide.SelectedPath != null || LeftSide.SelectedPath != null) ??
            if (RightSide.SelectedPath != null && LeftSide.SelectedPath != null)
                RightSide.SelectedPath = null;
        }

        //right sel
        private void RightSelectionChangeExecute(object obj)
        {
            if (LeftSide.SelectedPath != null && RightSide.SelectedPath != null)
                LeftSide.SelectedPath = null;
        }

        private void UpdateView()
        {
            LeftSide.UpdateCurrentPathContent();
            RightSide.UpdateCurrentPathContent();
        }

        #endregion
    }
}