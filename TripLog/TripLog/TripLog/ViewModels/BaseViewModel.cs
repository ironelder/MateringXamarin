using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TripLog.ViewModels
{
    public abstract class BaseViewModel:INotifyPropertyChanged
    {
        protected BaseViewModel()
        {
        }
        public abstract Task Init();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel():base()
        {
        }
        public override async Task Init()
        {
            await Init(default(TParameter));
        }
        public abstract Task Init(TParameter parameter);
    }

}
