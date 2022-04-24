using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp1.Model;
using System.ServiceProcess;
using System.Windows.Threading;
using System.Windows;
using System.Security.Principal;

namespace WpfApp1.ViewModel
{
    class WinServiceViewModel
    {
        private int _SelectedItemIndex;
        private IList<WinService> _WinServicesList;
        private WindowState _WindowState;

        DispatcherTimer dispatcherTimer;

        public WinServiceViewModel()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            _WinServicesList = new List<WinService>();
            ServiceController[] services = ServiceController.GetServices();
            _WinServicesList.Clear();
            foreach (ServiceController service in services)
            {
                _WinServicesList.Add(new WinService { 
                    Name = service.ServiceName, DisplayName = service.DisplayName, Status = service.Status, Account = service.MachineName });
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var newWinServicesList = new List<WinService>();
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                newWinServicesList.Add(new WinService
                {
                    Name = service.ServiceName,
                    DisplayName = service.DisplayName,
                    Status = service.Status,
                    Account = service.MachineName
                });
            }

            // updating statuses from fresh list
            for (int i = 0; i < _WinServicesList.Count; i++)
            {
                var oldService = _WinServicesList[i];
                var found = newWinServicesList.FindIndex(x => x.Name == oldService.Name);
                if (found == -1)
                {
                    _WinServicesList.RemoveAt(i);
                    i--;
                }
                else
                {
                    _WinServicesList[i].Status = newWinServicesList[found].Status;
                }
            }

            // adding new services if present
            if (_WinServicesList.Count < newWinServicesList.Count)
            {
                foreach (var service in newWinServicesList)
                {
                    var found = ((List<WinService>)_WinServicesList).FindIndex(x => x.Name == service.Name);
                    if (found == -1)
                    {
                        _WinServicesList.Add(service.Copy());
                    }
                }
            }
        }

        public WindowState WindowState 
        { 
            get 
            { 
                return _WindowState; 
            } 
            set 
            {
                _WindowState = value; 
                OnWindowStateChanged(); 
            } 
        }

        public int SelectedItemIndex
        {
            get { return _SelectedItemIndex; }
            set { _SelectedItemIndex = value;
                OnSelectedIndexChanged();
            }
        }

        public string SelectedItemName
        {
            get { return _WinServicesList[_SelectedItemIndex].Name; }
        }

        public IList<WinService> WinServices
        {
            get { return _WinServicesList; }
            set { _WinServicesList = value; }
        }

        public void OnWindowStateChanged()
        {
            if (_WindowState == WindowState.Minimized)
            {
                dispatcherTimer.Stop();
            }
            else
            {
                dispatcherTimer.Start();
            }
        }

        public void StartSelectedService()
        {
            var selectedService = _WinServicesList[_SelectedItemIndex];
            if (selectedService != null && selectedService.Status != ServiceControllerStatus.Running)
            {
                selectedService.Status = ServiceControllerStatus.Running;
            }
        }

        public void StopSelectedService()
        {
            var selectedService = _WinServicesList[_SelectedItemIndex];
            if (selectedService != null && selectedService.Status != ServiceControllerStatus.Stopped)
            {
                selectedService.Status = ServiceControllerStatus.Stopped;
            }
        }

        private void OnSelectedIndexChanged()
        {
            _StopperParameter.CurrentItemName = SelectedItemName;
            _StarterParameter.CurrentItemName = SelectedItemName;
        }

        private StopperParameter _StopperParameter { get; set; }
        public StopperParameter StopperParameter
        {
            get
            {
                if (_StopperParameter == null)
                {
                    _StopperParameter = new StopperParameter { CurrentItemName = SelectedItemName };
                    _StopperParameter.OnStop = new StopperParameter.OnStopMethod(StopSelectedService);
                }
                return _StopperParameter;
            }
            set { _StopperParameter = value; }
        }

        private StarterParameter _StarterParameter { get; set; }
        public StarterParameter StarterParameter
        {
            get
            {
                if (_StarterParameter == null)
                {
                    _StarterParameter = new StarterParameter { CurrentItemName = SelectedItemName };
                    _StarterParameter.OnStart = new StarterParameter.OnStartMethod(StartSelectedService);
                }
                return _StarterParameter;
            }
            set { _StarterParameter = value; }
        }

        private ICommand mStarter;
        public ICommand StartCommand
        {
            get
            {
                if (mStarter == null)
                    mStarter = new Starter();
                return mStarter;
            }
            set
            {
                mStarter = value;
            }
        }

        private ICommand mStopper;
        public ICommand StopCommand
        {
            get
            {
                if (mStopper == null)
                    mStopper = new Stopper();
                return mStopper;
            }
            set
            {
                mStopper = value;
            }
        }
    }
}
