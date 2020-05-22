using ChatApp.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp
{
    public partial class App : Application
    {
        DataClass dataClass = DataClass.GetInstance;
        public App()
        {
            InitializeComponent();

            if (dataClass.isSignedIn)
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
