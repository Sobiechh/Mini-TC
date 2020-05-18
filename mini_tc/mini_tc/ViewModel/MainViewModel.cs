using System;
using System.IO;

using mini_tc.Properties;
using System.Windows.Input;
//using Base;
//czemu nie dziala tutaj?

namespace mini_tc.ViewModel
{
    using Base; // Jeżeli Pan Doktor to widzi to proszę o wytłumaczenie czemu using tutaj
    using mini_tc.Model;

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
            if (LeftSide.SelectedPath != null)
            {
                source = Path.Combine(LeftSide.CurrentPath, LeftSide.GetCorrectSelectedPath());
                target = Path.GetFullPath(RightSide.CurrentPath);
            }

            copyModel.Copy(source, target);
        }

        private bool CopyCanExecute(object obj)
        {
            // check if previous clicked
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
        #endregion
    }
}