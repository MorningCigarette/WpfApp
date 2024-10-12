using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider Services { get; }


        public new static App Current => (App)Application.Current;

        public App()
        {
            Services = ConfigServices();
        }

        private static IServiceProvider ConfigServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<Dispatcher>(_ => Current.Dispatcher);
            services.AddSingleton<MainWindowViewModel>();
            return services.BuildServiceProvider();
        }
    }

}
