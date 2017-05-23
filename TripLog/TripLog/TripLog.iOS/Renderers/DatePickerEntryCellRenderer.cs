using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TripLog.Controls;
using TripLog.iOS.Renderers;

[assembly: ExportRenderer(typeof(DatePickerEntryCell), typeof(DatePickerEntryCellRenderer))]
namespace TripLog.iOS.Renderers
{
    public class DatePickerEntryCellRenderer:EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            var datepickerCell = (DatePickerEntryCell)item;

            UITextField textField = null;

            if(cell != null)
            {
                textField = (UITextField)cell.ContentView.Subviews[0];
            }

            var mode = UIDatePickerMode.Date;
            var displayFormat = "d";
            var date = NSDate.Now;
            var isLocalTime = false;

            if(datepickerCell != null)
            {
                if(datepickerCell.Date.Kind == DateTimeKind.Unspecified)
                {
                    var local = new DateTime(datepickerCell.Date.Ticks, DateTimeKind.Local);
                    date = (NSDate)local;
                }
                else
                {
                    date = (NSDate)datepickerCell.Date;
                }
                isLocalTime = datepickerCell.Date.Kind == DateTimeKind.Local || datepickerCell.Date.Kind == DateTimeKind.Unspecified;
            }

            var datepicker = new UIDatePicker
            {
                Mode = mode,
                BackgroundColor = UIColor.White,
                Date = date,
                TimeZone = isLocalTime ? NSTimeZone.LocalTimeZone : new NSTimeZone("UTC")
            };

            var done = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) =>
            {
                var pickedDate = (DateTime)datepicker.Date;

                if (isLocalTime)
                {
                    pickedDate = pickedDate.ToLocalTime();
                }
                if (datepickerCell != null)
                {
                    datepickerCell.Date = pickedDate;
                }
                if (textField != null)
                {
                    textField.Text = pickedDate.ToString(displayFormat);
                    textField.ResignFirstResponder();
                }
            });

            var toolbar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                Translucent = false
            };

            toolbar.SizeToFit();
            toolbar.SetItems(new[] { done }, true);

            if(textField != null)
            {
                textField.InputView = datepicker;
                textField.InputAccessoryView = toolbar;
                if (datepickerCell != null)
                    textField.Text = datepickerCell.Date.ToString(displayFormat);
            }

            return cell;
        }
    }
}