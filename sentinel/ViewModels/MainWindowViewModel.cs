using Prism.Mvvm;
using sentinel.Views;
using System;
using System.ComponentModel;

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


        private string _title = "Prism Unity Application";
        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            var test = new TestViewModel();
            CurrentView = test;
        }
    }
}
