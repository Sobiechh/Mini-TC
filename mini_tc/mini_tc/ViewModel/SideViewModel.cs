using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using mini_tc.Properties;
using System.Windows.Input;


namespace mini_tc.ViewModel
{
    using System.Collections.ObjectModel;
    using ViewModel.Base;

    class SideViewModel : BaseViewModel
    {
        #region Properties

        private string _currentPath;
        public string CurrentPath
        {
            get { return _currentPath; }
            set { _currentPath = value; OnPropertyChanged(nameof(CurrentPath)); }
        }

        private ObservableCollection<string> _availableDrives;
        public ObservableCollection<string> AvailableDrives
        {
            get { return _availableDrives; }
            set { _availableDrives = value; OnPropertyChanged(nameof(AvailableDrives)); }
        }
        private ObservableCollection<string> _currentPathContent;
        public ObservableCollection<string> CurrentPathContent
        {
            get { return _currentPathContent; }
            set { _currentPathContent = value; OnPropertyChanged(nameof(CurrentPathContent)); }
        }
        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value; OnPropertyChanged(nameof(SelectedPath)); }
        }
        private string _selectedDrive;
        public string SelectedDrive
        {
            get { return _selectedDrive; }
            set
            {
                _selectedDrive = value;
                OnPropertyChanged(nameof(SelectedDrive));
                CurrentPath = _selectedDrive;
                UpdateCurrentPathContent();
            }
        }

        #endregion

        #region Constructor
        public SideViewModel()
        {
            UpdateAvailableDrives();
            CurrentPathContent = new ObservableCollection<string>();
            //any func + lambda
            //x.Contains("C")).First() always disk containt C at first
            SelectedDrive = AvailableDrives.Any(x => x.Contains("C")) ? AvailableDrives.Where(x => x.Contains("C")).First() : AvailableDrives.First();

            DropDownOpen = new RelayCommand(DropDownOpenExecute, argument => true);
            ItemDoubleClick = new RelayCommand(ItemDoubleClickExecute, argument => true);
            ItemEnterKey = new RelayCommand(ItemEnterKeyExecute, ItemEnterKeyCanExecute);
        }
        #endregion

        #region Commands
        public ICommand ItemDoubleClick { get; set; }
        public ICommand ItemEnterKey { get; set; }

        public ICommand DropDownOpen { get; set; }

        private void DropDownOpenExecute(object o) => UpdateAvailableDrives();

        private void ItemDoubleClickExecute(object o) => EnterDirectory();

        private void ItemEnterKeyExecute(object o) => EnterDirectory();
        private bool ItemEnterKeyCanExecute(object o) => SelectedPath != null; // == throw except
        #endregion


        #region func
        private void EnterDirectory()
        {
            if (SelectedPath == null) return;//unless exception
            if (SelectedPath.StartsWith(Resources.DriveSign))
            {
                CurrentPath = Path.Combine(CurrentPath, SelectedPath.Substring(3)); //delete 4 chars path
                UpdateCurrentPathContent();
            }
            else if (SelectedPath == Resources.PreviousDirectory) //back
            {
                CurrentPath = Path.GetDirectoryName(CurrentPath);
                UpdateCurrentPathContent();
            }
        }
        private void UpdateAvailableDrives()
        {
            AvailableDrives = new ObservableCollection<string>(Directory.GetLogicalDrives().ToList());
        }

        public void UpdateCurrentPathContent()
        {
            CurrentPathContent.Clear();
            if (!AvailableDrives.Contains(CurrentPath))
            {
                CurrentPathContent.Add(Resources.PreviousDirectory);
            }
            foreach (var dir in GetDirectories(CurrentPath))
            {
                CurrentPathContent.Add(Resources.DriveSign + Path.GetFileName(dir));
            }
            foreach (var file in GetFiles(CurrentPath))
            {
                CurrentPathContent.Add(Path.GetFileName(file));
            }
        }

        private List<string> GetFiles(string path) //need current path to detect file location
        {
            var files_list = new List<string>();
            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    files_list.Add(file);
                }
            } // no access allowed
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return files_list;
        }

        private List<string> GetDirectories(string path)
        {
            var dirs_list = new List<string>();
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    dirs_list.Add(directory);
                }
            } // no access allowed
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return dirs_list;
        }

        //get path with lamda func, substring del (<C/D/E>)
        public string GetSelectedPath() => SelectedPath.StartsWith(Resources.DriveSign) ? SelectedPath.Substring(Resources.DriveSign.Length) : SelectedPath;

        #endregion
    }
}