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

namespace sentinel.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                //RaisePropertyChanged(() => CurrentView);
            }
        }
        private object _currentView; 

        private string _title = "Sentinelle";

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

        public MainWindowViewModel()
        {
            var test = new TestViewModel();
            CurrentView = test;
        }

        #region Commands
        public DelegateCommand ExitCommand { get { return _ExitCommand ?? (_ExitCommand = new DelegateCommand(Quit)); } }
        protected DelegateCommand _ExitCommand;

        #endregion

        #region Private Methods
        private void Quit()
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
