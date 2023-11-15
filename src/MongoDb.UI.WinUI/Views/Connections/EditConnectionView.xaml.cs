using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MongoDb.UI.ViewModels.Connections;


namespace MongoDb.UI.WinUI.Views.Connections
{
    public sealed partial class EditConnectionView : UserControl
    {
        public static readonly DependencyProperty SelectedConnectionProperty = DependencyProperty.Register(nameof(SelectedConnection), typeof(ConnectionViewModel), typeof(EditConnectionView), new PropertyMetadata(default(ConnectionViewModel?)));
        public static readonly DependencyProperty ConnectCommandProperty = DependencyProperty.Register(nameof(ConnectCommand), typeof(ICommand), typeof(EditConnectionView), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand), typeof(EditConnectionView), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SaveAndConnectCommandProperty = DependencyProperty.Register(nameof(SaveAndConnectCommand), typeof(ICommand), typeof(EditConnectionView), new PropertyMetadata(default(ICommand)));

        public EditConnectionView()
        {
            this.InitializeComponent();
        }

        public ConnectionViewModel? SelectedConnection
        {
            get => (ConnectionViewModel?)GetValue(SelectedConnectionProperty);
            set => SetValue(SelectedConnectionProperty, value);
        }

        public ICommand ConnectCommand
        {
            get => (ICommand)GetValue(ConnectCommandProperty);
            set => SetValue(ConnectCommandProperty, value);
        }

        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public ICommand SaveAndConnectCommand
        {
            get => (ICommand)GetValue(SaveAndConnectCommandProperty);
            set => SetValue(SaveAndConnectCommandProperty, value);
        }
    }
}
