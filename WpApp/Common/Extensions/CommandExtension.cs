using System;
using System.Windows.Input;

namespace WpApp.Common.Extensions
{
    public static class CommandExtension
    {
        public static void Raise(this ICommand command, object parameter)
        {
            if (command != null && command.CanExecute(parameter)) {
                command.Execute(parameter);
            }
        }

        public static void Raise(this ICommand command)
        {
            Raise(command, System.Reactive.Unit.Default);
        }
    }
}
