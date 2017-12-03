using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace BlokusGame.Models
{
    public class MyThumb : Thumb
    {
        public List<LineGeometry> EndLines { get; private set; }
        public List<LineGeometry> StartLines { get; private set; }

        public MyThumb() : base()
        {
            StartLines = new List<LineGeometry>();
            EndLines = new List<LineGeometry>();
        }
    }
}
