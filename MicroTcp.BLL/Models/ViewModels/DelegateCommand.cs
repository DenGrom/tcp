using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicroTcp.BLL.Models.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        //public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
//#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
//#pragma warning restore 67
    }
}
