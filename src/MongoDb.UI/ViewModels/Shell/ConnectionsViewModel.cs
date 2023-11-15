using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Mediator;
using MongoDb.Application.Connection.Commands;
using MongoDb.UI.Services.Contracts;
using MongoDb.UI.ViewModels.Connections;
using MongoDb.UI.ViewModels.Dialogs;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace MongoDb.UI.ViewModels.Shell;

public sealed class ConnectionsViewModel : ReactiveObject
{
    private bool _connected;
    private readonly SourceCache<ConnectionViewModel, Guid> _connections;
    private ReadOnlyObservableCollection<ConnectionViewModel> _savedConnections = new ReadOnlyObservableCollection<ConnectionViewModel>(new ObservableCollection<ConnectionViewModel>());
    private ReadOnlyObservableCollection<ConnectionViewModel> _recentConnections = new ReadOnlyObservableCollection<ConnectionViewModel>(new ObservableCollection<ConnectionViewModel>());
    private CompositeDisposable _disposables;

    private readonly IMediator _mediator;
    private bool _hasSavedConnections;
    private bool _hasRecentConnections;
    private ConnectionViewModel? _selectedConnection;

    public ConnectionsViewModel(IMediator mediator, IDialogService dialogService)
    {
        _mediator = mediator;
        _connections = new SourceCache<ConnectionViewModel, Guid>(x => x.Connection.Id);
        SelectedConnection = new ConnectionViewModel();

        IObservable<bool> canExecute = this.WhenAnyValue(x => x.SelectedConnection).Select(x => x != null);
        SaveCommand = ReactiveCommand.CreateFromTask(async (ConnectionViewModel vm) =>
        {
            var dialog = new SaveConnectionDialogViewModel(mediator, vm.Connection);
            var added = await dialogService.ShowDialogAsync(dialog);
            if (added.Result.Result)
            {
                vm.ConnectionName = dialog.ConnectionName ?? dialog.Connection.ConnectionString;
                vm.ConnectionColor = dialog.ConnectionColor;

                vm.IsSaved = true;
                _connections.AddOrUpdate(vm);
            }
        }, canExecute: canExecute);

        NewConnectionCommand = ReactiveCommand.Create(() =>
        {
            SelectedConnection = new ConnectionViewModel();
        });
    }

    public async Task Connect(CancellationToken ct)
    {
        if (_connected) return;

        var cmd = new GetAllConnectionsRequest();
        var cmdResult = await _mediator.Send(cmd, ct);

        foreach (var connection in cmdResult.Connections)
        {
            var vm = new ConnectionViewModel(
                connection: connection.Entity,
                isSaved: connection.IsSaved,
                order: connection.Order,
                lastUsed: connection.LastUsed.LocalDateTime,
                frequency: connection.Frequency);
            _connections.AddOrUpdate(vm);
        }

        _disposables = new CompositeDisposable();
        _connections.Connect()
            .Filter(x => x.IsSaved)
            .Sort(SortExpressionComparer<ConnectionViewModel>.Ascending(x => x.Order))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _savedConnections)
            .Subscribe()
            .DisposeWith(_disposables);

        _connections.Connect()
            .Filter(x => x.LastUsed is not null)
            .Sort(SortExpressionComparer<ConnectionViewModel>.Descending(x => x.LastUsed!))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _recentConnections)
            .Subscribe()
            .DisposeWith(_disposables);

        _connections.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .AutoRefresh(x => x.LastUsed)
            .ToCollection()
            .Select(x => x.Any(f => f.LastUsed is not null))
            .Subscribe(x => HasRecentConnections = x)
            .DisposeWith(_disposables);

        _connections.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .AutoRefresh(x => x.IsSaved)
            .ToCollection()
            .Select(x => x.Any(f => f.IsSaved))
            .Subscribe(x => HasSavedConnections = x)
            .DisposeWith(_disposables);

        _connected = true;
    }

    public void Disconnect()
    {
        if (!_connected) return;

        _connections.Clear();
        _disposables.Dispose();
        _disposables = null;

        _connected = false;
    }

    public bool HasSavedConnections
    {
        get => _hasSavedConnections;
        private set => this.RaiseAndSetIfChanged(ref _hasSavedConnections, value);
    }

    public bool HasRecentConnections
    {
        get => _hasRecentConnections;
        private set => this.RaiseAndSetIfChanged(ref _hasRecentConnections, value);
    }

    public ReadOnlyObservableCollection<ConnectionViewModel> SavedConnections => _savedConnections;
    public ReadOnlyObservableCollection<ConnectionViewModel> RecentConnections => _recentConnections;

    public ConnectionViewModel? SelectedConnection
    {
        get => _selectedConnection;
        set => this.RaiseAndSetIfChanged(ref _selectedConnection, value);
    }

    public ReactiveCommand<ConnectionViewModel, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> NewConnectionCommand { get; }
}