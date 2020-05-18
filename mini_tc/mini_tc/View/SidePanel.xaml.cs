using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace mini_tc.View
{
    //interface (cur_path,drivers,path content)
    interface ISidePanel
    {
        string CurrentPath { get; set; }
        ObservableCollection<string> AvailableDrives { get; set; }
        ObservableCollection<string> CurrentPathContent { get; set; }
    }

    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class SidePanel : UserControl, ISidePanel //ISidePanel interface
    {
        #region Dependencies
        protected static readonly DependencyProperty CurrentPathProperty = DependencyProperty.Register(
            nameof(CurrentPath), 
            typeof(string), 
            typeof(SidePanel), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty AvailableDrivesProperty = DependencyProperty.Register(
            nameof(AvailableDrives), 
            typeof(ObservableCollection<string>), 
            typeof(SidePanel), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty CurrentPathContentProperty = DependencyProperty.Register(
            nameof(CurrentPathContent), 
            typeof(ObservableCollection<string>), 
            typeof(SidePanel), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register(
            nameof(SelectedPath), 
            typeof(string), 
            typeof(SidePanel), 
            new PropertyMetadata(null));

        protected static readonly DependencyProperty SelectedDriveProperty = DependencyProperty.Register(
            nameof(SelectedDrive), 
            typeof(string), 
            typeof(SidePanel), 
            new PropertyMetadata(null));

        //ICommands

        protected static readonly DependencyProperty SelectionChangeProperty = DependencyProperty.Register(
            nameof(SelectionChange),
            typeof(ICommand),
            typeof(SidePanel),
            new PropertyMetadata(null));

        protected static readonly DependencyProperty ItemDoubleClickProperty = DependencyProperty.Register(
            nameof(ItemDoubleClick),
            typeof(ICommand),
            typeof(SidePanel),
            new PropertyMetadata(null));

        protected static readonly DependencyProperty DropDownOpenProperty = DependencyProperty.Register(
            nameof(DropDownOpen),
            typeof(ICommand),
            typeof(SidePanel),
            new PropertyMetadata(null));

        protected static readonly DependencyProperty ItemEnterKeyProperty = DependencyProperty.Register(
            nameof(ItemEnterKey),
            typeof(ICommand),
            typeof(SidePanel),
            new PropertyMetadata(null));

        #endregion

        #region Props
        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }
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
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }
        public string SelectedDrive
        {
            get { return (string)GetValue(SelectedDriveProperty); }
            set { SetValue(SelectedDriveProperty, value); }
        }
        #endregion

        #region constr
        public SidePanel()
        {
            InitializeComponent();
        }
        #endregion

        #region Commands
        public ICommand DropDownOpen
        {
            get { 
                return (ICommand)GetValue(DropDownOpenProperty);
            }
            set {
                SetValue(DropDownOpenProperty, value);
            }
        }

        public ICommand SelectionChange
        {
            get { 
                return (ICommand)GetValue(SelectionChangeProperty); 
            }
            set { 
                SetValue(SelectionChangeProperty, value); 
            }
        }
        
        public ICommand ItemDoubleClick
        {
            get { 
                return (ICommand)GetValue(ItemDoubleClickProperty); 
            }
            set { 
                SetValue(ItemDoubleClickProperty, value); 
            }
        }
        
        public ICommand ItemEnterKey
        {
            get { 
                return (ICommand)GetValue(ItemEnterKeyProperty); 
            }
            set { 
                SetValue(ItemEnterKeyProperty, value); 
            }
        }
        #endregion
    }
}