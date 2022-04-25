using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class StarterParameter
    {
        public string CurrentItemName { get; set; }
        public delegate void OnStartMethod();
        public OnStartMethod OnStart { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class Starter : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var starterParameter = (StarterParameter)parameter;
            if (starterParameter == null)
            {
                throw new ArgumentNullException(nameof(starterParameter));
            }
            var service = new ServiceController(starterParameter.CurrentItemName);
            if (service.Status != ServiceControllerStatus.Running && starterParameter.IsAdmin)
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                starterParameter.OnStart();
            }
        }

        #endregion
    }
}
