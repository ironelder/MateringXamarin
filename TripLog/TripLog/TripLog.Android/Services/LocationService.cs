using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TripLog.Services;
using Xamarin.Forms;
using TripLog.Models;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace TripLog.Droid.Services
{
    public class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 30;

            var position = await locator.GetPositionAsync(30000);
            var result = new GeoCoords
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };

            return result;
        }

    }
}