using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TripLog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        DetailViewModel _vm
        {
            get { return BindingContext as DetailViewModel; }
        }

        readonly Map _map;

        public DetailPage()
        {
            InitializeComponent();
            Title = "Entry Details";
            //BindingContext = new DetailViewModel(DependencyService.Get<INavService>());
            BindingContextChanged += (sender, args) =>
            {
                if (_vm == null) return;

                _vm.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Entry")
                    {
                        UpdateMap();
                    }
                };
            };
            var mainLayout = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = new GridLength(4, GridUnitType.Star)
                    },
                    new RowDefinition
                    {
                        Height = GridLength.Auto
                    },
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };

            //var map = new Map();
            _map = new Map();

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(entry.Latitude, entry.Longitude), Distance.FromMiles(.5)));
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_vm.Entry.Latitude, _vm.Entry.Longitude), Distance.FromMiles(.5)));

            //map.Pins.Add(new Pin
            //{
            //    Type = PinType.Place,
            //    //Label = entry.Title,
            //    Label = _vm.Entry.Title,
            //    //Position = new Position(entry.Latitude, entry.Longitude)
            //    Position = new Position(_vm.Entry.Latitude, _vm.Entry.Longitude)
            //});



            var title = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            //title.Text = entry.Title;
            title.SetBinding(Label.TextProperty, "Entry.Title");

            var date = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            //date.Text = entry.Date.ToString("M");
            date.SetBinding(Label.TextProperty, "Entry.Date", stringFormat: "{0:M}");

            var rating = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            //rating.Text = $"{entry.Rating} star rating";
            rating.SetBinding(Label.TextProperty, "Entry.Rating", stringFormat: "{0} star rating");

            var notes = new Label
            {
                HorizontalOptions = LayoutOptions.Center
            };
            //notes.Text = entry.Notes;
            notes.SetBinding(Label.TextProperty, "Entry.Notes");

            var details = new StackLayout
            {
                Padding = 10,
                Children =
                {
                    title, date, rating, notes
                }
            };

            var detailsBg = new BoxView
            {
                BackgroundColor = Color.White,
                Opacity = .8
            };

            //mainLayout.Children.Add(map);
            mainLayout.Children.Add(_map);
            mainLayout.Children.Add(detailsBg, 0, 1);
            mainLayout.Children.Add(details, 0, 1);

            //Grid.SetRowSpan(map, 3);
            Grid.SetRowSpan(_map, 3);

            void UpdateMap()
            {
                if(_vm.Entry == null)
                {
                    return;
                }

                _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_vm.Entry.Latitude, _vm.Entry.Longitude), Distance.FromMiles(.5) ));
                _map.Pins.Add(new Pin
                {
                    Type = PinType.Place,
                    Label = _vm.Entry.Title,
                    Position = new Position(_vm.Entry.Latitude, _vm.Entry.Longitude)
                });
            }
            Content = mainLayout;
        }
    }
}