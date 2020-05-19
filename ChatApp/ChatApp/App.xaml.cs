using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var isLoggedIn = Properties.ContainsKey("isLoggedIn") ? (bool)Properties["isLoggedIn"] : false;

            if (isLoggedIn)
            {
                MainPage = new Pages.MainPage();
            }
            else
            {
                MainPage = new NavigationPage(new Pages.LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
