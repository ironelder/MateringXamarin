﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Plugin.Geolocator;

namespace TripLog.iOS.Services
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
