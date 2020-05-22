using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ChatApp.Model;
using ChatApp.Helpers;
using ChatApp.DependencyServices;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            lbl_Name.Text = dataClass.loggedInUser.name;
            lbl_Email.Text = dataClass.loggedInUser.email;
        }

        private async void SignOut(object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = DependencyService.Get<iFirebaseAuth>().SignOut();

            if (res.Status == true)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", res.Response, "Okay");
            }
            
        }
    }
}