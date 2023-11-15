using System.Reactive;
using ReactiveUI;

namespace MongoDb.UI.ViewModels.Dialogs.Base;

/// <summary>
/// CommonBase class.
/// </summary>
public abstract partial class DialogViewModelBase : ReactiveObject
{
    private bool _isDialogOpen;

    public DialogViewModelBase()
    {
        CancelCommand = ReactiveCommand.Create(() =>
        {
            Close(DialogResultKind.Cancel);
        });

    }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    public ReactiveCommand<Unit, Unit> OnNextCommand { get; set; }

    public bool IsDialogOpen
    {
        get => _isDialogOpen;
        set => this.RaiseAndSetIfChanged(ref _isDialogOpen, value);
    }

    public abstract void Close(DialogResultKind kind = DialogResultKind.Normal);

    public string Title { get; protected set; }
    public string GoNextText { get; protected set; }
    public string CancelText { get; protected set; }

}