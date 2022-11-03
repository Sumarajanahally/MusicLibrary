using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public static bool IsLoggedIn { get; set; }
         public static string UserName { get; set; }

        public LoginPage()
        {
            this.InitializeComponent();
            IsLoggedIn = false;
        }

        
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            UserName = UsernameTextBox.Text;
            string ps = Passwordbox.Password.ToString();

            if(UserName != null &&  ps == "test")
            {
                IsLoggedIn = true;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                ErrorMessage.Text = "Invalid User Id/ Invaid Credentials";
            }


        }
        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ErrorMessage.Text = "";
        }
    }
}
