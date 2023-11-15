using Windows.Storage;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace MongoDb.UI.WinUI
{
    public partial class App : Microsoft.UI.Xaml.Application
    {
        private readonly ServiceProvider _serviceProvider;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            var sp = new ServiceCollection()
                .AddMediator()
                .AddMongoDb(ApplicationData.Current.LocalFolder.Path)
                .BuildServiceProvider();
            _serviceProvider = sp;
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();

            m_window = new MainWindow(mediator);
            m_window.Activate();
        }

        private Window m_window;
    }
}
