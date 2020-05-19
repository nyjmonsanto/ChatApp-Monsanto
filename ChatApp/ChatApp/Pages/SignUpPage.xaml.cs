using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        //Navigation 
        private async void LoginPageNavigate(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //Loading Screen
        void ToggleIndicator(bool show)
        {
            bg.IsVisible = show;
            actvty_indctr.IsEnabled = show;
            actvty_indctr.IsVisible = show;
            actvty_indctr.IsRunning = show;
        }

        //Show/Hide Password
        public void ShowHidePass(object sender, EventArgs e)
        {
            Password.IsPassword = Password.IsPassword ? false : true;
            btn_ShowHide1.Text = Password.IsPassword ? "Show" : "Hide";

            ConfirmPassword.IsPassword = ConfirmPassword.IsPassword ? false : true;
            btn_ShowHide2.Text = ConfirmPassword.IsPassword ? "Show" : "Hide";
        }

        //Changes Border Color for Entry when focused
        public void ChangeColor(object sender, EventArgs e)
        {
            if (Name.IsFocused)
            {
                Frame1.BorderColor = Color.Black;
            }

            if (Email.IsFocused)
            {
                Frame2.BorderColor = Color.Black;
            }

            if (Password.IsFocused)
            {
                Frame3.BorderColor = Color.Black;
            }

            if (ConfirmPassword.IsFocused)
            {
                Frame4.BorderColor = Color.Black;
            }
        }

        //Sign Up
        private async void SignUpAction(object sender, EventArgs e)
        {
            ToggleIndicator(true);
            await Task.Delay(2500);
            await DisplayAlert("Success", "Sign up successful. Verification email sent.", "Okay");
            ToggleIndicator(false);
            await Navigation.PopAsync();
        }

        public void SignUpProcess(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                if (string.IsNullOrEmpty(Name.Text))
                {
                    Frame1.BorderColor = Color.Red;
                }

                if (string.IsNullOrEmpty(Email.Text))
                {
                    Frame2.BorderColor = Color.Red;
                }

                if (string.IsNullOrEmpty(Password.Text))
                {
                    Frame3.BorderColor = Color.Red;
                }

                if (string.IsNullOrEmpty(ConfirmPassword.Text))
                {
                    Frame4.BorderColor = Color.Red;
                }

                DisplayAlert("Error", "Missing field/s", "Okay");
            }
            else if (!Password.Text.Equals(ConfirmPassword.Text))
            {
                DisplayAlert("Error", "Passwords don't match", "Okay");
            }
            else
            {
                SignUpAction(sender, e);
            }
        }
    }
}