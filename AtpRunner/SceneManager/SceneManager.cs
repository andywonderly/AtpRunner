using AtpRunner.Components;
using AtpRunner.Entities;
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
        private MainGame mainGame;
        public Scene Scene { get; set; }
        public Point Camera { get; set; }
        public override string Name { get; set; }
        public delegate void InGameMenuCalledEvenHandler(object sender, EventArgs e);
        public event InGameMenuCalledEvenHandler InGameMenuCalled;
        public delegate void GameOverEventHandler(object sender, EventArgs e);
        public event GameOverEventHandler GameOver;

        public SceneManager(MainGame Game) : base(Game)
        {
            Scene = new Scene(); // How to re-do this?  Try to avoid New in constructors
            mainGame = Game;
            Name = "Scene";
            Initialize();
        }

        protected override void Initialize()
        {
            InitialLoad();
            base.Initialize();
        }

        public override void LoadContent()
        {
            //Add player, obstactles, props to scene
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Update(gameTime);
        }

        public void InitialLoad()
        {
            LoadLevel1();
        }

        public void LoadLevel1()
        {
            Scene = new Scene();
            var level1 = new Level1Loader(this, Scene);     
                   
        }
    }
}
