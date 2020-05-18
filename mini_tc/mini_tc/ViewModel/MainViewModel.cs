using System;

namespace mini_tc.ViewModel
{
    using Base;
    using System.Windows.Input;
    class MainViewModel : BaseViewModel
    {
        private SideViewModel _source;
        public SideViewModel Source
        {
            get { return _source; }
            set { _source = value; OnPropertyChanged(nameof(Source)); }
        }

        private SideViewModel _target;
        public SideViewModel Target
        {
            get { return _target; }
            set { _target = value; OnPropertyChanged(nameof(Target)); }
        }

        public MainViewModel()
        {
            Source = new SideViewModel();
            Target = new SideViewModel();

            Copy = new RelayCommand(CopyExecute, CopyCanExecute);
        }

        //copy files
        public ICommand Copy { get; set; }


        //console view
        private void CopyExecute(object obj)
        {
            Console.WriteLine(Source.SelectedDrive);
        }

        //help with copy
        private bool CopyCanExecute(object obj) 
        {
            return Source.SelectedPath == null; 
        }
    }
}