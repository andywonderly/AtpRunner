using AtpRunner.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Menu
{
    public abstract class Menu
    {
        public Scene.Scene ParentScene;
        public List<MenuItem> MenuItems;
        public MenuItem SelectedMenuItem;

        public Menu(Scene.Scene scene)
        {
            ParentScene = scene;

            LoadMenuItems();
            SelectedMenuItem = MenuItems.FirstOrDefault();
        }
        public void Update(KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Enter))
            {
                Activate();
            }
            else if(keyboardState.IsKeyDown(Keys.Up))
            {
                var index = MenuItems.IndexOf(SelectedMenuItem);
                
                if(index == 0)
                {
                    SelectedMenuItem = MenuItems.Last();
                }
                else
                {
                    SelectedMenuItem = MenuItems[index - 1];
                }
            }
            else if(keyboardState.IsKeyDown(Keys.Down))
            {
                var index = MenuItems.IndexOf(SelectedMenuItem);

                if(index == MenuItems.Count() - 1)
                {
                    SelectedMenuItem = MenuItems.First();
                }
                else
                {
                    SelectedMenuItem = MenuItems[index + 1];
                }
            }

            Draw();
        }

        public void End()
        {

        }

        private void Draw()
        {
            var renderManager = (RenderManager)ParentScene.SceneManager.MainGame.GetManager("Render");
            renderManager.DrawMenuItems(MenuItems, SelectedMenuItem);
        }

        public virtual void Activate() { }
        public virtual void LoadMenuItems() { }
    }

    public class MainMenu : Menu
    {
        public MainMenu(Scene.Scene scene) : base(scene)
        {

        }
        public override void LoadMenuItems()
        {
            MenuItems.Clear();

            var startGame = new MenuItem(this, "Start");
            var exitGame = new MenuItem(this, "Exit");

            SelectedMenuItem = startGame;
        }

        public override void Activate()
        {
            switch (SelectedMenuItem.Name)
            {
                case "Start":
                    // Load gameplay scene
                    break;
                case "Exit":
                    // Exit the program
                    break;
            }
        }
    }

    public class InGameMenu : Menu
    {
        public InGameMenu(Scene.Scene scene) : base(scene)
        {
            LoadMenuItems();
        }
        public override void LoadMenuItems()
        {
            MenuItems.Clear();

            var exitToMainMenu = new MenuItem(this, "Exit to Main Menu");

            SelectedMenuItem = exitToMainMenu;

            base.LoadMenuItems();
        }

        public override void Activate()
        {
            // Load main menu scene
        }
    }
}
