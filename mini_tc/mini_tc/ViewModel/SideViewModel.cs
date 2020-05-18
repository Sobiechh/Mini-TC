using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;


namespace mini_tc.ViewModel
{
    class SideViewModel : MainViewModel
    {
        using ViewModel.Base;

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
                UpdateCurrentPathContent();
            }
        }

        public SideViewModel()
        {
            AvailableDrives = new ObservableCollection<string>(Directory.GetLogicalDrives().ToList());
            Console.WriteLine(AvailableDrives);
            SelectedDrive = AvailableDrives.Any(x => x.Contains("s ")) ? AvailableDrives.Where(x => x.Contains("C")).First() : AvailableDrives.First();
            UpdateCurrentPathContent();
        }

        private void UpdateCurrentPathContent()
        {
            //help
            //Console.WriteLine(SelectedDrive);
            CurrentPathContent = new ObservableCollection<string>();
            foreach (var dir in Directory.GetDirectories(SelectedDrive))
            {
                CurrentPathContent.Add("<D> " + Path.GetFileName(dir));
            }
            foreach (var file in Directory.GetFiles(SelectedDrive))
            {
                CurrentPathContent.Add(Path.GetFileName(file));
            }
        }

        private void UpdateAvailabeDrives()
        {

        }
    }
}