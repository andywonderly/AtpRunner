using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public class MenuItem
    {
        public string Name;

        public MenuItem(Menu parentMenu, string name)
        {
            Name = name;
            parentMenu.MenuItems.Add(this);
        }
    }
}
