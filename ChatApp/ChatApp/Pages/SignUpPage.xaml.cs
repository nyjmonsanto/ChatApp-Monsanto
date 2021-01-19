using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ChatApp.Models;
using ChatApp.Helpers;
using ChatApp.DependencyServices;
using Plugin.CloudFirestore;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public SignUpPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
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

        //For Google and Facebook Sign-in buttons
        //Will only display an alert since the functionalities of the two buttons are optional
        private async void ButtonAction(object sender, EventArgs e)
        {
            ToggleIndicator(true);
            await Task.Delay(2500);
            ToggleIndicator(false);
            await DisplayAlert("", "Functionalities for these buttons have not yet been implemented.", "Okay");
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

        public async void SignUpProcess(object sender, EventArgs e)
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

                await DisplayAlert("Error", "Missing fields.", "Okay");
            }
            else if (!Password.Text.Equals(ConfirmPassword.Text))
            {
                await DisplayAlert("Error", "Passwords don't match.", "Okay");
                ConfirmPassword.Text = string.Empty;
                ConfirmPassword.Focus();
            }
            else
            {
                ToggleIndicator(true);

                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(Name.Text, Email.Text, Password.Text);

                if (res.Status == true)
                {
                    try
                    {
                        await CrossCloudFirestore.Current
                                                 .Instance
                                                 .Collection("users")
                                                 .Document(dataClass.loggedInUser.uid)
                                                 .SetAsync(dataClass.loggedInUser);
                        await DisplayAlert("Success", res.Response, "Okay");
                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "Okay");
                    }
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }

                ToggleIndicator(false);
            }
        }
    }
}