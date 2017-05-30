using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace sentinel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Set default culture to Fr
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
