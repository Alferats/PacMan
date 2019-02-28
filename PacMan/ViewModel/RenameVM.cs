using Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacMan.ViewModel
{
    class RenameVm
    {
        public Action OkAction { get; set; }
        public Action CancelAction { get; set; }

        public RenameVm(string currentName = "")
        {
            Name = currentName;
        }
        
        public string Name { get; set;}

        private RelayCommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ??
                       (_okCommand = new RelayCommand(obj =>
                       {
                           OkAction();
                       }));
            }
        }

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = new RelayCommand(obj =>
                       {
                           CancelAction();
                       }));
            }
        }
    }
}
