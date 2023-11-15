using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;
using MongoDb.UI.Services.Contracts;
using MongoDb.UI.ViewModels.Shell;

namespace MongoDb.UI.ViewModels;

public sealed class MainWindowViewModel
{
    public MainWindowViewModel(IMediator mediator, IDialogService dialogService)
    {
        Connections = new ConnectionsViewModel(mediator, dialogService);
        Instance = this;
    }
    public ConnectionsViewModel Connections { get; }

    public static MainWindowViewModel Instance { get; private set; }
}