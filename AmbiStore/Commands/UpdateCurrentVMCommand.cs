using AmbiStore.State.Navigators;
using AmbiStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AmbiStore.Commands
{
    public class UpdateCurrentVMCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        public UpdateCurrentVMCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if ((string)parameter == "Objetos")
            {
                _navigator.CurrentViewModel = new CONTATOViewModel();
            }
        }
    }
}
