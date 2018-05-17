using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public abstract class BaseMenu
    {
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public MenuItem CurrentMenuItem { get; set; }
        public AtpRunner.Scene.Scene Scene { get; set; }
        public Point Cursor { get; set; }

        public BaseMenu(AtpRunner.Scene.Scene scene)
        {
            Scene = scene;
        }

        protected virtual void Initialize()
        {
            MenuItem firstItem = MenuItems.FirstOrDefault();
            CurrentMenuItem = firstItem;
            Cursor = new Point(firstItem.X, firstItem.Y);
        }

        public void Update()
        {

        }
    }
}
