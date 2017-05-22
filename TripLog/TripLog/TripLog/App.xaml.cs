using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripLog.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new TripLog.MainPage());
            var mainPage = new NavigationPage(new MainPage());
            //var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
            //navService.XamarinFormsNav = mainPage.Navigation;
            //navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            //navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            //navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));
            Kernel = new StandardKernel(new TripLogCoreModule(),
                new TripLogNavModule(mainPage.Navigation));
            Kernel.Load(platformModules);
            mainPage.BindingContext = Kernel.Get<MainViewModel>();
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
