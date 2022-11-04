using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Shapes;

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
            // string path = $"/Assets/Music";
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string pathmusic = root + "/Assets/Music";
            string pathimage = root + "/Assets/CoverImages";
            string[] filesMusic = Directory.GetFiles(pathmusic, "*.mp3",SearchOption.AllDirectories);
            string[] filesImage = Directory.GetFiles(pathimage, "*.jpg", SearchOption.AllDirectories);
            string musicname, imagename;
            MusicCategory category;

           
            if (filesMusic.Length > 0)
            {
                foreach (string filemusic in filesMusic)
                {
                    musicname = System.IO.Path.GetFileNameWithoutExtension(filemusic);
                    //System.IO.FileInfo fi = null;
                    string Directory = System.IO.Path.GetDirectoryName(filemusic);
                    string FolderName = System.IO.Path.GetFileName(Directory);
                    foreach (string fileimage in filesImage)
                    {
                        
                        
                        if(FolderName == "Pop")
                        {
                            category = MusicCategory.Pop;
                        } 
                        else
                        {
                            category = MusicCategory.Kids;
                        }

                       imagename = System.IO.Path.GetFileNameWithoutExtension(fileimage);
                       
                        if(imagename == musicname)
                        {
                            songs.Add(new Music(musicname,category,filemusic,fileimage)); 

                            
                        }
                    }
                }
            } 
            // $"/Assets/Music/{category}/{name}.mp3";
            // $"/Assets/CoverImages/{category}/{name}.jpg";


          //  songs.Add(new Music("Test", MusicCategory.Pop));
           //songs.Add(new Music("Test2", MusicCategory.Kids));
           // songs.Add(new Song("Gun", SoundCategory.Cartoons));
           // songs.Add(new Song("Spring", SoundCategory.Cartoons));
            
          

            return songs;
        }
        // AddFavList()
        //RemoveFav()

        

      

        public static List<Music> getMyPlaylist()
        {
            var myfavlist = new List<Music>();

           // myfavlist = myfavlist.Add( )
           return myfavlist;

        }
    }
}
