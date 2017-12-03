using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlokusGame.Settings
{
    public static class BoardCellsSettings
    {
        public static double minHeight { get; set; } = 10d;
        public static double maxHeight { get; set; } = 100d;
        public static double minWidth { get; set; } = 10d;
        public static double maxWidth { get; set; } = 100d;
    }
}
