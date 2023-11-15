using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MongoDb.UI.ViewModels.Connections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MongoDb.UI.WinUI.Views.Connections
{
    public sealed partial class SaveConnectionDialogView : UserControl
    {
        public SaveConnectionDialogView()
        {
            this.InitializeComponent();
        }

        public SaveConnectionDialogViewModel ViewModel => (SaveConnectionDialogViewModel)DataContext;

    }
}
