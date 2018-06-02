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
        public enum LevelState
        {
            MainMenu,
            Level1,
            Level2,
            Level3,
            Win,
        }

        public MainGame MainGame;
        public Scene Scene { get; set; }
        public override string Name { get; set; }
        public LevelState Level { get; set; }

        public SceneManager(MainGame Game) : base(Game)
        {
            Scene = new Scene(this); // How to re-do this?  Try to avoid New in constructors
            MainGame = Game;
            Name = "Scene";
            Level = LevelState.MainMenu;
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
            Level = LevelState.Level1;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            
            var level1 = new LevelLoader(this, Scene, "Level1.json");
            renderManager.LoadContent();
        }

        private void LoadLevel2()
        {
            Level = LevelState.Level2;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");

            var level1 = new LevelLoader(this, Scene, "Level2.json");
            renderManager.LoadContent();
        }

        private void LoadLevel3()
        {
            Level = LevelState.Level3;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");

            var level1 = new LevelLoader(this, Scene, "Level3.json");
            renderManager.LoadContent();
        }

        private void LoadWinScreen()
        {
            Level = LevelState.Win;

            Scene = new Scene(this);
            Scene.Menu = new WinMenu(Scene);
            Scene.MenuActive = true;

            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            renderManager.LoadContent();
        }

        public void ReloadLevel()
        {
            switch (Level)
            {
                case LevelState.Level1:
                    LoadLevel1();
                    break;
                case LevelState.Level2:
                    LoadLevel2();
                    break;
                case LevelState.Level3:
                    LoadLevel3();
                    break;
            }
        }

        public void LevelComplete()
        {
            // Eventually we will do other stuff besides just load the next level.
            // Like have a win message, special animation, etc.  I envision at least
            // having the camera slow down or stop and the player riding out of camera
            // view.

            


            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            switch(Level)
            {
                case LevelState.Level1:
                    LoadLevel2();
                    break;
                case LevelState.Level2:
                    LoadLevel3();
                    break;
                case LevelState.Level3:
                    LoadWinScreen();
                    break;
            }
        }


    }
}
