using System.ComponentModel;
using System.Threading;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MongoDb.UI.ViewModels;
using MongoDb.UI.ViewModels.Connections;
using MongoDb.UI.ViewModels.Shell;

namespace MongoDb.UI.WinUI.Views.Connections;

public sealed partial class ConnectionsView : UserControl
{
    public ConnectionsView()
    {
        ViewModel = MainWindowViewModel.Instance.Connections;
        this.DataContext = ViewModel;
        this.InitializeComponent();
        this.DataContext = ViewModel;

        ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.SelectedConnection))
        {
            if (ViewModel.SelectedConnection.ConnectionName is "New Connection")
            {
                SavedConnectionsLv.SelectedItem = null;
            }
            else
            {
                SavedConnectionsLv.SelectedItem = ViewModel.SelectedConnection;
            }
        }
    }

    public ConnectionsViewModel ViewModel { get; }

    private async void ConnectionsView_OnLoaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.Connect(CancellationToken.None);
    }

    private void SavedConnectionsLv_OnItemClick(object sender, ItemClickEventArgs e)
    {
        ViewModel.SelectedConnection = (ConnectionViewModel)e.ClickedItem;
    }
}