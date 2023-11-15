using Mediator;
using MongoDb.Domain.Connection;
using MongoDb.UI.ViewModels.Dialogs.Base;
using ReactiveUI;

namespace MongoDb.UI.ViewModels.Connections;

public sealed class SaveConnectionDialogViewModel : DialogViewModelBase<DialogResult<bool>>
{
    private readonly IMediator _mediator;
    private string _connectionColor;
    private string _connectionName;

    public SaveConnectionDialogViewModel(IMediator mediator, MongoConnectionEntity connection)
    {
        _mediator = mediator;
        Connection = connection;

        Title = "Save Connection";
        GoNextText = "Save";
        CancelText = "Cancel";

        OnNextCommand = ReactiveCommand.Create(() =>
        {
            Close(DialogResultKind.Normal, new DialogResult<bool>(true, DialogResultKind.Normal));
        });
    }
    public MongoConnectionEntity Connection { get; }

    public string ConnectionColor
    {
        get => _connectionColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _connectionColor, value);
            Connection.Color = value;
        }
    }

    public string ConnectionName
    {
        get => _connectionName;
        set
        {
            this.RaiseAndSetIfChanged(ref _connectionName, value);
            Connection.Name = value;
        }
    }

    public override void Close(DialogResultKind kind = DialogResultKind.Normal)
    {
        base.Close(kind, default);
    }
}