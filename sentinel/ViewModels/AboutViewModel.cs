using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sentinel.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        public AboutViewModel()
        {

        }

        public string Version
        {
            get
            {
                Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

                System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(CurrentAssembly.Location);
                _Version = " - " + fileVersionInfo.FileVersion + " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return _Version;
            }
        }
        protected string _Version;

    }
}
