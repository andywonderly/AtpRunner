using AtpRunner.Components;
using AtpRunner.Entities;
using AtpRunner.Menu;
using AtpRunner.Render;
using AtpRunner.SceneLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Scene
{
    public class SceneManager : BaseManager
    {
        public MainGame MainGame;
        public Scene Scene { get; set; }
        public override string Name { get; set; }

        public SceneManager(MainGame Game) : base(Game)
        {
            Scene = new Scene(this); // How to re-do this?  Try to avoid New in constructors
            MainGame = Game;
            Name = "Scene";
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            LoadMainMenu();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Update(gameTime);
        }

        public void LoadMainMenu()
        {
            Scene = new Scene(this);
            Scene.Menu = new MainMenu(Scene);
            Scene.MenuActive = true;

            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            renderManager.LoadContent();
        }

        public void LoadLevel1()
        {
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            
            var level1 = new Level1Loader(this, Scene);
            renderManager.LoadContent();
        }


    }
}
