using AtpRunner.Components;
using AtpRunner.Entities;
using AtpRunner.Menu;
using AtpRunner.Render;
using AtpRunner.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Render
{
    public class RenderManager : BaseManager
    {
        private MainGame _mainGame;

        public GraphicsDeviceManager Graphics
        {
            get { return this.Game.Graphics; }
            set { this.Game.Graphics = value; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return this.Game.GraphicsDevice; }
        }

        public SpriteBatch SpriteBatch { get; private set; }
        public override string Name { get; set; }

        private List<Texture2D> _sceneTextures { get; set; }

        public RenderManager(MainGame game) : base(game)
        {
            Name = "Render";
            _mainGame = game;
            _sceneTextures = new List<Texture2D>();
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            SceneManager sceneManager = (SceneManager)_mainGame.GetManager("Scene");
            var entities = sceneManager.Scene.GetEntitiesWithSprites();
            foreach (var entity in entities)
            {
                RenderComponent renderComponent = (RenderComponent)entity.GetComponent("Render");
                var texture = _mainGame.Content.Load<Texture2D>(renderComponent.TextureName);
                _sceneTextures.Add(texture);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Draw(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SceneManager sceneManager = this.Game.GetManager("Scene") as SceneManager;
            if(sceneManager == null)
            {
                throw new Exception("Scene manager not properly registered to game engine.");
            }

            Point camera = sceneManager.Scene.Camera;
            if(camera == null)
            {
                throw new Exception("Camera does not exist in game scene.");
            }

            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            List<BaseEntity> entities = sceneManager.Scene.GetEntitiesWithSprites();

            SpriteBatch.Begin();

            foreach (BaseEntity entity in entities)
            {
                if (entity.Components.Any(n => n.Name == "Render"))
                {
                    RenderComponent component = (RenderComponent)entity.Components.FirstOrDefault(n => n.Name == "Render");
                    Texture2D texture = _sceneTextures.FirstOrDefault(n => n.Name == component.TextureName);
                    int translatedX = entity.X - camera.X;
                    int translatedY = (int)entity.Y - camera.Y;

                    Rectangle frame = new Rectangle(translatedX, translatedY, 
                        component.Dimensions.X, component.Dimensions.Y);
                    
                    SpriteBatch.Draw(texture, frame, Color.White);
                }
            }

            if(sceneManager.Scene.MenuActive)
            {
                var menuItems = sceneManager.Scene.Menu.MenuItems;
                var selectedItem = sceneManager.Scene.Menu.SelectedMenuItem;

                DrawMenuItems(menuItems, selectedItem);
            }

            SpriteBatch.End();
        }

        private void DrawMenuItems(List<MenuItem> menuItems, MenuItem selectedItem)
        {
            //SpriteBatch.Begin();

            var font = _mainGame.Content.Load<SpriteFont>("AtpSpriteFont");            

            var menuX = 200;
            var menuY = 200;
            var increment = 36;

            foreach (MenuItem item in menuItems)
            {
                var color = Color.Black;
                var prefix = "    ";

                if(item == selectedItem)
                {
                    color = Color.Yellow;
                    prefix = "-> ";
                }

                SpriteBatch.DrawString(font, prefix + item.Name, new Vector2(menuX, menuY), color);

                menuY += increment;
            }

            //SpriteBatch.End();
        }
    }
}
