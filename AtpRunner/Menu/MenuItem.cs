using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public class MenuItem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }
        public string SpriteName { get; set; }

        public MenuItem(int x, int y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }
    }
}
