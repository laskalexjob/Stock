using System.Windows;
using Autofac;
using Stock.UI.Startup;

namespace Stock.UI
{
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new Bootstrapper().Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }
    }
}
