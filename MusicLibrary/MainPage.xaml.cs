using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using MusicLibrary.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Popups;
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
      //  private Music selectedSong;
        private ObservableCollection<Music> playList;

        public MainPage()
        {
            this.InitializeComponent();

            if (LoginPage.IsLoggedIn == false)
            {
                Loaded += MainPage_Loaded;
            }
            else
            {
                UserName.Text = LoginPage.UserName;
            }
            songs = new ObservableCollection<Music>();
            playList = new ObservableCollection<Music>();
            MusicManager.GetAllSongs(songs);

            menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem { IconFile = "Assets/Icons/Instruments.png", Category = MusicCategory.Instruments });
            menuItems.Add(new MenuItem { IconFile = "Assets/Icons/Kids.png", Category = MusicCategory.Kids });
            menuItems.Add(new MenuItem { IconFile = "Assets/Icons/Myplaylist.png", Category = MusicCategory.MyPlaylist });
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
            MusicGridView.ItemsSource = songs;
            BackButton.Visibility = Visibility.Collapsed;
            MenuItemListView.SelectedItem = null;


        }

        private void MusicGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Music)e.ClickedItem;
            //selectedSong = sound;

            MusicMedia.Source = new Uri(this.BaseUri, sound.AudioFile);
            MusicMedia.PosterSource = new BitmapImage(new Uri(this.BaseUri, sound.ImageFile));
            SimplepopTextBlock.Text = sound.Name;

            if (MenuItemListView.SelectedItem != null)
            {
                if (sound.Category == MusicCategory.MyPlaylist)
                {
                    AddPlaylist.Visibility = Visibility.Collapsed;
                    RemovePlayList.Visibility = Visibility.Visible;
                }
                else
                {
                    AddPlaylist.Visibility = Visibility.Visible;
                    RemovePlayList.Visibility = Visibility.Collapsed;
                }

            }
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }

            // MusicplayerMedia.Source = MediaSource.CreateFromUri(new Uri(this.BaseUri, sound.AudioFile));
        }

        private void MenuItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;

            TextBlock.Text = menuItem.Category.ToString();

            if (menuItem.Category == MusicCategory.MyPlaylist)
            {
                //var playlistitems = new List<String>();
                
                playList.Clear();
                var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData
                    ), "*.playlist.txt");
                foreach (var file in files)
                {

                    var songnm = File.ReadAllText(file);
                    
                   // playlistitems.Add(songnm);
                    var filteredSongs = songs.Where(song => song.Name == songnm).ToList();
                    filteredSongs.ForEach(song => playList.Add(new Music(song.Name, MusicCategory.MyPlaylist, song.AudioFile, song.ImageFile)));

                }

                MusicGridView.ItemsSource = playList;
            }
            else
            {
                MusicGridView.ItemsSource = songs;
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



        private void ChangeCoverButton_Click(object sender, RoutedEventArgs e)
        {
            //string songName = SimplepopTextBlock.Text;

        }

        private async void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
     
            string song = SimplepopTextBlock.Text;
            // playList.Add(new Music(selectedSong));
            //  string user = UserName.Text;

            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.playlist.txt");
            File.WriteAllText(filename, song);


            /*  if (playList.Where(song => song.Name == songn).Count() == 0)
              {
                  var filteredSongs = songs.Where(song => song.Name == songn).ToList();
                  filteredSongs.ForEach(song => playList.Add(new Music(song.Name, MusicCategory.MyPlaylist, song.AudioFile, song.ImageFile)));
              } */
            var dialog = new MessageDialog("Song added to PlayList successfully");
            await dialog.ShowAsync();


        }

        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var songMenuFlyoutSubItem = new MenuFlyoutSubItem()
            {
                Text = "AddToPlayList"
            };


        }

        private async void RemovePlayList_Click(object sender, RoutedEventArgs e)
        {
            string playingsong = SimplepopTextBlock.Text;
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData
                ), "*.playlist.txt");
            foreach (var file in files)
            {

                var songnm = File.ReadAllText(file);
                if (playingsong == songnm)
                {
                    File.Delete(file);
                }

            }

            var dialog = new MessageDialog("Song removed from PlayList successfully");
            await dialog.ShowAsync();
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }



            playList.Clear();
            var remainingfiles = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData
                ), "*.playlist.txt");
            foreach (var file in remainingfiles)
            {

                var songnm = File.ReadAllText(file);

                // playlistitems.Add(songnm);
                var filteredSongs = songs.Where(song => song.Name == songnm).ToList();
                filteredSongs.ForEach(song => playList.Add(new Music(song.Name, MusicCategory.MyPlaylist, song.AudioFile, song.ImageFile)));

            }

            MusicGridView.ItemsSource = playList;


        }
    }
}
