﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel:BaseViewModel
    {
        readonly ILocationService _locService;
        string _title;
        Command _saveCommand;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(ExcuteSaveCommand, CanSave));
            }
        }

        void ExcuteSaveCommand()
        {
            var newItem = new TripLogEntry
            {
                Title = this.Title,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Date = this.Date,
                Rating = this.Rating,
                Notes = this.Notes
            };
        }

        bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        //public NewEntryViewModel()
        //{
        //    Date = DateTime.Today;
        //    Rating = 1;
        //}
        public NewEntryViewModel(INavService navService, ILocationService locService) : base(navService)
        {
            _locService = locService;
            Date = DateTime.Today;
            Rating = 1;
        }


        async Task ExecuteSaveCommand()
        {
            var newItem = new TripLogEntry
            {
                Title = this.Title,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Date = this.Date,
                Rating = this.Rating,
                Notes = this.Notes
            };

            await NavService.GoBack();
        }

        public override async Task Init()
        {
            var coords = await _locService.GetGeoCoordinatesAsync();
            Latitude = coords.Latitude;
            Longitude = coords.Longitude;
        }
    }
}
