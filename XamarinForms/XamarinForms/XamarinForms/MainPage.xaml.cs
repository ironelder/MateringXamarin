using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Models;

namespace XamarinForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();
            var listView = new ListView
            {
                RowHeight = 40
            };
            //listView.ItemsSource = new string[]
            //{
            //    "Buy pears",
            //    "Buy oranges",
            //    "Buy mangos",
            //    "Buy apples",
            //    "Buy bananas"
            //};
            listView.ItemsSource = new TodoItem[]
            {
                new TodoItem{Name="Buy pears"},
                new TodoItem{Name="Buy oranges", Done=true },
                new TodoItem{Name="Buy mangos"},
                new TodoItem{Name="Buy apples", Done=true},
                new TodoItem{Name="Buy bananas", Done=true}
            };
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    listView
                }
            };
        }
    }
}
