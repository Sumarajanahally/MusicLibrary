using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLibrary.Model
{
    internal static class MusicManager
    {

        public static void GetAllSongs(ObservableCollection<Music> songs)
        {
            var allSongs = getSongs();
            songs.Clear();
            allSongs.ForEach(song => songs.Add(song));
           
        }
        public static void GetAllSongsByCategory(ObservableCollection<Music> songs,
            MusicCategory category)
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(song => song.Category == category).ToList();
            filteredSongs.ForEach(song => songs.Add(song));
        }

        private static List<Music> getSongs()
        {
            var songs = new List<Music>();

            // MusicCategory category;

           // $"/Assets/Music/{category}/{name}.mp3";
          // $"/Assets/CoverImages/{category}/{name}.jpg";


            songs.Add(new Music("Test", MusicCategory.Pop));
            songs.Add(new Music("Test2", MusicCategory.Kids));
           // songs.Add(new Song("Gun", SoundCategory.Cartoons));
           // songs.Add(new Song("Spring", SoundCategory.Cartoons));
            
          

            return songs;
        }
       // AddFavList()
       //RemoveFav()
        public static List<Music> getMyFavoriteList()
        {
            var myfavlist = new List<Music>();
            
           // myfavlist = myfavlist.Add( )
            return myfavlist;

        }
    }
}
