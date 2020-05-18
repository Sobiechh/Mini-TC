using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace mini_tc.View
{
    /// <summary>
    /// Logika interakcji dla klasy SideView.xaml
    /// </summary>
    public partial class SideView : UserControl
    {
        protected static readonly DependencyProperty CurrentPathProperty = DependencyProperty.Register(
            nameof(CurrentPath), 
            typeof(string), 
            typeof(SideView), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty AvailableDrivesProperty = DependencyProperty.Register(
            nameof(AvailableDrives), 
            typeof(ObservableCollection<string>), 
            typeof(SideView),
            new PropertyMetadata(null));


        protected static readonly DependencyProperty CurrentPathContentProperty = DependencyProperty.Register(
            nameof(CurrentPathContent), 
            typeof(ObservableCollection<string>), 
            typeof(SideView), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register(
            nameof(SelectedPath), 
            typeof(string), 
            typeof(SideView), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty SelectedDriveProperty = DependencyProperty.Register(
            nameof(SelectedDrive),
            typeof(string), 
            typeof(SideView), 
            new PropertyMetadata(null));

        //current visible path
        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        //using observable collection for drivers and path content
        public ObservableCollection<string> AvailableDrives
        {
            get { return (ObservableCollection<string>)GetValue(AvailableDrivesProperty); }
            set { SetValue(AvailableDrivesProperty, value); }
        }

        public ObservableCollection<string> CurrentPathContent
        {
            get { return (ObservableCollection<string>)GetValue(CurrentPathContentProperty); }
            set { SetValue(CurrentPathContentProperty, value); }
        }

        // selected path
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        // selected drive
        public string SelectedDrive
        {
            get { return (string)GetValue(SelectedDriveProperty); }
            set { SetValue(SelectedDriveProperty, value); }
        }

        public SideView()
        {
            InitializeComponent();
        }
    }
}
