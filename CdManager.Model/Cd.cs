using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CdManager.Model
{
    public class Cd
    {
        public string Artist { get; set; }
        public string AlbumTitle { get; set; }
        public List<Track> Tracks { get; set; }

        public Cd()
        {
            Tracks = new List<Track>();
        }
    }
}
