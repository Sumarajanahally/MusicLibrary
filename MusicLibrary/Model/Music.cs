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
        Pop,
        Kids,
        MyPlaylist
    }
    internal class Music
    {
        public string Name { get; set; }

        public string AudioFile { get; set; }

        public string ImageFile { get; set; }

        public MusicCategory Category { get; set; }


        public Music( string name,MusicCategory category)
        {
            Name = name;
            AudioFile = $"/Assets/Music/{category}/{name}.mp3";
            ImageFile = $"/Assets/CoverImages/{category}/{name}.jpg"; ;
            Category = category;
        }


    }
}
