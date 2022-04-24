using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceProcess;

namespace WpfApp1.Model
{
    public class WinService : INotifyPropertyChanged
    {
        private string name;
        private string displayName;
        private ServiceControllerStatus status;
        private string account;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        public string DisplayName
        {
            get { return displayName; }
            set 
            { 
                displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public ServiceControllerStatus Status
        {
            get { return status; }
            set 
            { 
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string Account
        {
            get { if (account == ".") return "Local System"; return account; }
            set 
            { 
                account = value;
                OnPropertyChanged(nameof(Account));
            }
        }

        public WinService Copy()
        {
            return new WinService { Name = this.Name, DisplayName = this.DisplayName, Status = this.Status, Account = this.Account };
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
