using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TripLog.Controls
{
    public class DatePickerEntryCell:EntryCell
    {
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
        public static readonly BindableProperty DateProperty = BindableProperty.Create<DatePickerEntryCell, DateTime>(p =>
            p.Date,
            DateTime.Now,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate<DateTime>(DatePropertyChanged)
        );
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty);  }
            set { SetValue(DateProperty, value); }
        }
        public new event EventHandler Completed;

        static void DatePropertyChanged(BindableObject bindable, DateTime oldValue, DateTime newValue)
        {
            var @this = (DatePickerEntryCell)bindable;

            if(@this.Completed != null)
            {
                @this.Completed(bindable, new EventArgs());
            }
        }

    }
}
