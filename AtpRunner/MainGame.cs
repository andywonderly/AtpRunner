using AtpRunner.Render;
using AtpRunner.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using LevelParser;

namespace AtpRunner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        public GraphicsDeviceManager Graphics { get; set; }
        //SpriteBatch spriteBatch;
        //SpriteBatch moved to RenderManager.

        public List<BaseManager> Managers { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Managers = new List<BaseManager>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SceneManager sceneManager = new SceneManager(this);
            RenderManager renderManager = new RenderManager(this);

            base.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public void AddManager(BaseManager manager)
        {
            if (Managers.Any(m => m.Name == manager.Name))
            {
                throw new Exception("Manager type " + manager.Name + " already exists within the game engine.");
            }

            Managers.Add(manager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        public BaseManager GetManager(string managerName)
        {
            BaseManager manager = null;
            if (Managers.Any(m => m.Name == managerName))
            {
                manager = Managers.FirstOrDefault(m => m.Name == managerName);
            }
            else
            {
                throw new Exception("Manager named " + managerName + " not found.");
            }
            
            return manager;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            //spriteBatch stuff moved to RenderManager.

            // TODO: use this.Content to load your game content here
            foreach(BaseManager manager in Managers)
            {
                manager.LoadContent();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            foreach(BaseManager manager in Managers)
            {
                manager.UnloadContent();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            //Input logic moved to player update

            // TODO: Add your update logic here

            //base.Update(gameTime);
            //Moved to...?

            foreach (BaseManager manager in Managers)
            {
                manager.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            foreach(BaseManager manager in Managers)
            {
                manager.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
