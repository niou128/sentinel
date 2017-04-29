using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sentinel.ViewModels
{
    public class TestViewModel : BindableBase
    {
        #region Binding Properties
        private string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set { SetProperty(ref _FilePath, value); }
        }
        
        #endregion

        #region constructor
        public TestViewModel()
        {
            FilePath = String.Empty;
        }
        #endregion
    }
}
