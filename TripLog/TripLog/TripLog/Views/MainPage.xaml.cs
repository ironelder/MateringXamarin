using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog
{
    public partial class MainPage : ContentPage
    {
        MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }
        public MainPage()
        {
            InitializeComponent();
            Title = "TripLog";
            //BindingContext = new MainViewModel(DependencyService.Get<INavService>());
            //var items = new List<TripLogEntry>
            //{
            //    new TripLogEntry
            //    {
            //        Title = "Washington Monument",
            //        Notes = "Amazing!",
            //        Rating = 3,
            //        Date = new DateTime(2015, 2, 5),
            //        Latitude = 38.8895,
            //        Longitude = -77.0352
            //    },
            //    new TripLogEntry
            //    {
            //        Title = "Statue of Liberty",
            //        Notes = "Inspiring!",
            //        Rating = 4,
            //        Date = new DateTime(2015, 4, 13),
            //        Latitude = 40.6892,
            //        Longitude = -74.0444
            //    },
            //    new TripLogEntry
            //    {
            //        Title = "Golden Gate Bridge",
            //        Notes = "Foggy, but beautiful.",
            //        Rating = 5,
            //        Date = new DateTime(2015, 4, 26),
            //        Latitude = 37.8268,
            //        Longitude = -122.4798
            //    }
            //};

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            var entries = new ListView
            {
                //ItemsSource = items,
                ItemTemplate = itemTemplate
            };
            entries.SetBinding(ListView.ItemsSourceProperty, "LogEntries");
            var newButton = new ToolbarItem
            {
                Text = "New"
            };
            //newButton.Clicked += (sender, e) =>
            //{
            //    Navigation.PushAsync(new NewEntryPage());
            //};
            //ToolbarItems.Add(newButton);
            newButton.SetBinding(ToolbarItem.CommandProperty, "NewCommand");

#pragma warning disable CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.
            entries.ItemTapped += async (sender, e) =>
            {
                var item = (TripLogEntry)e.Item;
                //await Navigation.PushAsync(new DetailPage(item));
                _vm.ViewCommand.Execute(item);
            };
#pragma warning restore CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.

            Content = entries;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_vm != null)
            {
                await _vm.Init();
            }
        }
    }
}
