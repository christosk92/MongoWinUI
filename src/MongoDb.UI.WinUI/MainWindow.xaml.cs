using System;
using System.Threading.Tasks;
using Mediator;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using MongoDb.UI.Services.Contracts;
using MongoDb.UI.ViewModels;
using MongoDb.UI.ViewModels.Dialogs.Base;
using MongoDb.UI.WinUI.Views.Dialogs;

namespace MongoDb.UI.WinUI;
public sealed partial class MainWindow : Window
{
    public MainWindow(IMediator mediator)
    {
        var dialogService = new WinUIDialogService(this);
        ViewModel = new MainWindowViewModel(mediator, dialogService);
        this.InitializeComponent();
        this.SystemBackdrop = new MicaBackdrop();
        this.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
    }

    public MainWindowViewModel ViewModel { get; }
}

public class WinUIDialogService : IDialogService
{
    private readonly MainWindow _mainWindow;
    public WinUIDialogService(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public async Task<DialogResult<TResult>> ShowDialogAsync<TResult>(DialogViewModelBase<TResult> viewModel)
    {
        var valueConverter = (IValueConverter)Microsoft.UI.Xaml.Application.Current.Resources["ContentDialogViewSelector"];
        var dialog = new DialogContentView
        {
            Title = viewModel.Title,
            PrimaryButtonText = viewModel.GoNextText,
            SecondaryButtonText = viewModel.CancelText,
            Content = valueConverter.Convert(viewModel, null, null, null),
            DataContext = viewModel,
            VerticalContentAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            XamlRoot = _mainWindow.Content.XamlRoot
        };
        await dialog.ShowAsync();
        var res = await viewModel.GetDialogResultAsync();
        return res;
    }
}


