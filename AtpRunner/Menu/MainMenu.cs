using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public class MainMenu : BaseMenu
    {
        public MainMenu(Scene.Scene scene) : base(scene)
        {
            Name = "MainMenu";
            Initialize();
        }

        protected override void Initialize()
        {
            var newGame = new MenuItem(10, 10, "New Game");
            MenuItems.Add(newGame);
            var quitGame = new MenuItem(10, 30, "Exit Game");
            MenuItems.Add(quitGame);

            base.Initialize();
        }
    }
}
