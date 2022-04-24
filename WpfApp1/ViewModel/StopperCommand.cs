using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class StopperParameter
    {
        public string CurrentItemName { get; set; }
        public delegate void OnStopMethod();
        public OnStopMethod OnStop { get; set; }
    }

    public class Stopper : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var stopperParameter = (StopperParameter)parameter;
            if (stopperParameter == null)
            {
                throw new ArgumentNullException(nameof(stopperParameter));
            }
            var service = new ServiceController(stopperParameter.CurrentItemName);
            if (service.CanStop)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));
                stopperParameter.OnStop();
            }
        }

        #endregion
    }
}
