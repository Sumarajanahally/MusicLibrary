using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
             var Username = UsernameTextBox.Text;
            string ps = Passwordbox.Password.ToString();

            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
           Username+ "user.txt");
            try
            { 
              StreamWriter sw = new System.IO.StreamWriter(filename);
             sw.WriteLine(Username + "\n" + ps);

              sw.Close();
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
              System.IO.Directory.CreateDirectory("C:\\" + Username);
             StreamWriter sw = new System.IO.StreamWriter(filename);
             sw.WriteLine(Username + "\n" + ps);

             sw.Close();
            }
            var dialog = new MessageDialog(" successfully registered");
            await dialog.ShowAsync();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //LoginPage.IsLoggedIn = false;
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
