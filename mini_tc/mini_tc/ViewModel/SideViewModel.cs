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

            DropDownOpen = new RelayCommand(DropDownOpenExecute, arg => true);
            ItemDoubleClick = new RelayCommand(ItemDoubleClickExecute, arg => true);
            ItemEnterKey = new RelayCommand(ItemEnterKeyExecute, ItemEnterKeyCanExecute);
        }
        #endregion

        #region Commands
        public ICommand DropDownOpen { get; set; }
        public ICommand ItemDoubleClick { get; set; }
        public ICommand ItemEnterKey { get; set; }

        private void DropDownOpenExecute(object obj) => UpdateAvailableDrives();

        private void ItemDoubleClickExecute(object obj) => EnterDirectory();

        private void ItemEnterKeyExecute(object obj) => EnterDirectory();
        private bool ItemEnterKeyCanExecute(object obj) => SelectedPath != null; // == throw except
        #endregion

        #region functionality
        private void EnterDirectory()
        {
            if (SelectedPath.StartsWith(Resources.DriveSign))
            {
                CurrentPath = Path.Combine(CurrentPath, SelectedPath.Substring(4)); //delete 4 chars path
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

        private void UpdateCurrentPathContent()
        {
            CurrentPathContent.Clear();
            if (!AvailableDrives.Contains(CurrentPath))
            {
                CurrentPathContent.Add("...");
            }
            //foreach (var dir in GetDirectories()) test no arg
            foreach (var dir in GetDirectories(CurrentPath)) //where we are
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
            var files = new List<string>();
            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    files.Add(file);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return files;
        }

        private List<string> GetDirectories(string path)
        {
            var directories = new List<string>();
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    directories.Add(directory);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            return directories;
        }

        //get path with lamda func, substring del (<C/D/E>)
        public string GetCorrectSelectedPath() => SelectedPath.StartsWith(Resources.DriveSign) ? SelectedPath.Substring(Resources.DriveSign.Length) : SelectedPath;

        #endregion
    }
}