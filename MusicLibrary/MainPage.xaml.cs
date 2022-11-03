using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MusicLibrary.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> songs;
        private List<MenuItem> menuItems;

        public MainPage()
        {
            this.InitializeComponent();

            if (LoginPage.IsLoggedIn == false)
            {
                Loaded += MainPage_Loaded;
            }
            songs = new ObservableCollection<Music>();
            MusicManager.GetAllSongs(songs);

            menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem { IconFile = "Assets/Icons/animals.png", Category = MusicCategory.Pop });
            menuItems.Add(new MenuItem { IconFile = "Assets/Icons/cartoon.png", Category = MusicCategory.Kids });
            BackButton.Visibility = Visibility.Collapsed;

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            ContentSplitView.IsPaneOpen = !ContentSplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.GetAllSongs(songs);
            TextBlock.Text = "All Music";
            BackButton.Visibility = Visibility.Collapsed;
            MenuItemListView.SelectedItem = null;
            

        }

        private void MusicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Music)e.ClickedItem;
           MusicMedia.Source = new Uri(this.BaseUri, sound.AudioFile);
            MusicMedia.PosterSource = new BitmapImage(new Uri(this.BaseUri, sound.ImageFile));
            SimplepopTextBlock.Text = sound.Name;
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }

            // MusicplayerMedia.Source = MediaSource.CreateFromUri(new Uri(this.BaseUri, sound.AudioFile));
        }

        private void MenuItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem  = (MenuItem)e.ClickedItem;
            TextBlock.Name = menuItem.Category.ToString();

            if (menuItem.Category == MusicCategory.MyPlaylist)
            { 
            }
            else
            {
                MusicManager.GetAllSongsByCategory(songs, menuItem.Category);
            }
            BackButton.Visibility = Visibility.Visible;

        }


        // Handles the Click event on the Button inside the Popup control and 
        // closes the Popup. 
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        // Handles the Click event on the Button on the page and opens the Popup. 
        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeCoverButton_Click(object sender, RoutedEventArgs e)
        {
           string songName = SimplepopTextBlock.Text;

        }
    }
}
