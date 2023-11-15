using MongoDb.UI.ViewModels.Dialogs.Base;

namespace MongoDb.UI.Services.Contracts;

public interface IDialogService
{
    Task<DialogResult<TResult>> ShowDialogAsync<TResult>(DialogViewModelBase<TResult> viewModel);
}