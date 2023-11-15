using System;
using Microsoft.UI.Xaml.Data;
using MongoDb.UI.ViewModels.Dialogs.Base;

namespace MongoDb.UI.WinUI.Selectors;

public sealed class ContentDialogViewSelector : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var name = value.GetType().FullName.Replace("ViewModel", "View");
        //MongoDb.UI.ViewModels.Dialogs.SaveConnectionDialogViewModel
        // ->
        //MongoDb.UI.WinUI.Views.Dialogs.SaveConnectionDialogView
        name = name.Replace("MongoDb.UI", "MongoDb.UI.WinUI");

        var type = Type.GetType(name);
        if (type == null)
        {
            throw new InvalidOperationException($"Cannot find view for {value.GetType().Name}");
        }
        var view = Activator.CreateInstance(type);
        return view;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}