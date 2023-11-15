using Microsoft.UI.Xaml.Controls;
using MongoDb.UI.ViewModels.Dialogs.Base;


namespace MongoDb.UI.WinUI.Views.Dialogs
{
    public sealed partial class DialogContentView : ContentDialog
    {
        public DialogContentView()
        {
            this.InitializeComponent();
            this.Closing += OnClosing;
        }
        public DialogViewModelBase ViewModel => (DialogViewModelBase)DataContext;

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (ViewModel.IsDialogOpen)
            {
                args.Cancel = true;
            }
        }
    }
}
