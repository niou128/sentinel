using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using sentinel.Views;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace sentinel.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private Properties
        #endregion

        #region Public Properties
        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Binding Properties
        public string Title
        {
            get
            {
                Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

                System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(CurrentAssembly.Location);
                _title += " - " + fileVersionInfo.FileVersion + " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return _title;
            }
            set { SetProperty(ref _title, value); }
        }
        private string _title = "Sentinelle";

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                 SetProperty(ref _currentView, value); 
                //_currentView = value;
                //RaisePropertyChanged("CurrentView");
            }
        }
        private object _currentView;

        public object TestModel
        {
            get { return _testModel; }
            set
            {
                SetProperty(ref _testModel, value);
                //_currentView = value;
                //RaisePropertyChanged("CurrentView");
            }
        }
        private object _testModel;
        

        public bool AboutFlyoutIsOpen
        {
            get { return _AboutFlyoutIsOpen; }
            set
            {
                SetProperty(ref _AboutFlyoutIsOpen, value);
            }
        }
        private bool _AboutFlyoutIsOpen;


        public string OpenFilePath
        {
            get { return _OpenFilePath; }
            set { SetProperty(ref _OpenFilePath, value); }
        }
        protected string _OpenFilePath;


        public vmSurveillance Surveillance
        {
            get { return _Surveillance; }
            set { SetProperty(ref _Surveillance, value); }
        }
        protected vmSurveillance _Surveillance;

        #endregion

        #region Commands
        public DelegateCommand ExitCommand { get { return _ExitCommand ?? (_ExitCommand = new DelegateCommand(Quit)); } }
        protected DelegateCommand _ExitCommand;

        public DelegateCommand CommandAbout { get { return _CommandAbout ?? (_CommandAbout = new DelegateCommand(Help)); } }
        protected DelegateCommand _CommandAbout;

        public DelegateCommand CommandOpenFile { get { return _CommandOpenFile ?? (_CommandOpenFile = new DelegateCommand(OpenFile)); } }
        protected DelegateCommand _CommandOpenFile;

        public DelegateCommand CommandOpenFolder { get { return _CommandOpenFolder ?? (_CommandOpenFolder = new DelegateCommand(OpenFolder)); } }
        protected DelegateCommand _CommandOpenFolder;

        public DelegateCommand CommandStart { get { return _CommandStart ?? (_CommandStart = new DelegateCommand(Start)); } }
        protected DelegateCommand _CommandStart;

        public DelegateCommand CommandStop { get { return _CommandStop ?? (_CommandStop = new DelegateCommand(Stop)); } }
        protected DelegateCommand _CommandStop;

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            AboutFlyoutIsOpen = false;
            Surveillance = new vmSurveillance();
            CurrentView = Surveillance;
            TestModel = new AboutViewModel();
        }
        #endregion

        #region Private Methods
        //private void RaisePropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        /// <summary>
        /// Quitte l'application
        /// </summary>
        private void Quit()
        {
            Application.Current.Shutdown();
        }

        private void Help()
        {
            AboutFlyoutIsOpen = true;
            //CurrentView = new AboutViewModel();
        }

        /// <summary>
        /// Sélectionne le fichier à surveiller
        /// </summary>
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                OpenFilePath = openFileDialog.FileName;
                Surveillance.OpenFilePath = OpenFilePath;
                Surveillance.OpenDirectoryPath = String.Empty;
            }
        }

        /// <summary>
        /// Sélectionne le dossier à surveiller
        /// </summary>
        private void OpenFolder()
        {
            if (CommonOpenFileDialog.IsPlatformSupported)
            {
                var folderSelectorDialog = new CommonOpenFileDialog();
                folderSelectorDialog.EnsureReadOnly = true;
                folderSelectorDialog.IsFolderPicker = true;
                folderSelectorDialog.AllowNonFileSystemItems = false;
                folderSelectorDialog.Multiselect = false;
                folderSelectorDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderSelectorDialog.Title = "Choissisez un dossier";
                folderSelectorDialog.ShowDialog();

                Surveillance.OpenDirectoryPath = folderSelectorDialog.FileName;
                Surveillance.OpenFilePath = String.Empty;
            }
        }

        /// <summary>
        /// Démarre la surveillance
        /// </summary>
        private void Start()
        {
            if (String.IsNullOrEmpty(Surveillance.OpenFilePath) && String.IsNullOrEmpty(Surveillance.OpenDirectoryPath))
            {
                MessageBox.Show("Vous devez choisir un fichier ou un dossier avant de démarrer la surveillance", "Impossible de démarrer", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Surveillance.StartSurveillance();
        }

        

        private void Stop()
        {
            if (!Surveillance.IsSurveillanceActive)
            {
                MessageBox.Show("Aucune surveillance en cours", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else
            {
                Surveillance.StopSurveillance();
            }
        }
        #endregion
    }
}
