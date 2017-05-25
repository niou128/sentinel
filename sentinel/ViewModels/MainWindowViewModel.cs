﻿using MahApps.Metro.Controls;
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

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                OpenFilePath = openFileDialog.FileName;
                Surveillance.OpenFilePath = OpenFilePath;
            }
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
        #endregion
    }
}
