using AtpRunner.Components;
using AtpRunner.Entities;
using AtpRunner.Menu;
using AtpRunner.Render;
using AtpRunner.SceneLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            Level = LevelState.MainMenu;
            Scene = new Scene(this);
            Scene.Menu = new MainMenu(Scene);
            Scene.MenuActive = true;
            Scene.SceneIsMenu = true;
            var menuMenuLevel = new MainMenuLevelLoader(this, Scene, "MainMenu");
            var texture = Game.Content.Load<Texture2D>("Tekken5Stage");
            Scene.Background = new Background(Scene, texture, 0);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            renderManager.LoadContent();
        }

        public void LoadLevel1()
        {
            Level = LevelState.Level1;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            var texture = Game.Content.Load<Texture2D>("Venice");
            Scene.Background = new Background(Scene, texture, 0);
            Scene.SceneIsMenu = false;
            
            var level1 = new LevelLoader(this, Scene, "Level1.json");
            renderManager.LoadContent();
        }

        private void LoadLevel2()
        {
            Level = LevelState.Level2;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            var texture = Game.Content.Load<Texture2D>("Sydney");
            Scene.Background = new Background(Scene, texture, 0);
            Scene.SceneIsMenu = false;

            var level1 = new LevelLoader(this, Scene, "Level2.json");
            renderManager.LoadContent();
        }

        private void LoadLevel3()
        {
            Level = LevelState.Level3;
            Scene = new Scene(this);
            Scene.Menu = new InGameMenu(Scene);
            var renderManager = (RenderManager)Scene.SceneManager.MainGame.GetManager("Render");
            var texture = Game.Content.Load<Texture2D>("Chaiwan");
            Scene.Background = new Background(Scene, texture, 0);
            Scene.SceneIsMenu = false;

            var level1 = new LevelLoader(this, Scene, "Level3.json");
            renderManager.LoadContent();
        }

        private void LoadWinScreen()
        {
            Level = LevelState.Win;

            Scene = new Scene(this);
            Scene.Menu = new WinMenu(Scene);
            Scene.MenuActive = true;
            Scene.SceneIsMenu = true;

            var winGuy = new BaseEntity(this, "WinGuy", 150, 0);
            var winGuyRender = new RenderComponent(winGuy, "atpGood", 1, 1);
            var winGuyInput = new WinGuyInputComponent(winGuy);

            Scene.AddEntityToScene(winGuy);

            var texture = Game.Content.Load<Texture2D>("ResidentEvilWin");
            Scene.Background = new Background(Scene, texture, 0);

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

            if(Level == LevelState.Level3)
            {
                var trumpet = MainGame.Content.Load<SoundEffect>("Trumpet");
                trumpet.Play();
            }
            else
            {
                var bagoosh = MainGame.Content.Load<SoundEffect>("Bagoosh");
                bagoosh.Play();
            }
            
            

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
