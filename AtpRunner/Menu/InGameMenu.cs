using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public class InGameMenu : BaseMenu
    {
        public InGameMenu(Scene.Scene scene) : base(scene)
        {
            Name = "InGameMenu";
            Initialize();
        }

        protected override void Initialize()
        {
            var newGame = new MenuItem(10, 10, "Quit To Main Menu");
            MenuItems.Add(newGame);
            var quitGame = new MenuItem(10, 30, "Exit Game");
            MenuItems.Add(quitGame);

            base.Initialize();
        }
    }
}
