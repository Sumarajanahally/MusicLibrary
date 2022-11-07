using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicLibrary.Model
{
    internal enum MusicCategory
    {
        Instruments,
        Kids,
        MyPlaylist
    }
    internal class Music
    {
        public string Name { get; set; }

        public string AudioFile { get; set; }

        public string ImageFile { get; set; }

        public MusicCategory Category { get; set; }


        public Music( string name,MusicCategory category,string audioFile,string imageFile)
        {
            Name = name;
            Category = category;
            AudioFile = audioFile;// $"/Assets/Music/{category}/{name}.mp3";
            ImageFile = imageFile; //$"/Assets/CoverImages/{category}/{name}.jpg"; ;
            
        }


    }
}
