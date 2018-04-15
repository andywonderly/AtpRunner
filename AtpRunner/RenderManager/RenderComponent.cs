using AtpRunner.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;
using Microsoft.Xna.Framework;

namespace AtpRunner.Render
{
    public class RenderComponent : BaseComponent
    {
        public int CurrentFrame { get; set; }
        public string TextureName { get; set; }
        // We'll get the render dimensions and position from RenderComponent for now.
        // "Frame" below.
        public Point Dimensions { get; set; }
        public RenderComponent(BaseEntity parentEntity, string textureName, int width, int height) : base(parentEntity)
        {
            Name = "Render";
            TextureName = textureName;
            CurrentFrame = 1;
            //TotalFrames = info gotten from sprite sheet's accompanying XML.
            Dimensions = new Point(width, height);
            Initialize();
        }

        protected override void Initialize()
        {
            // Load XML animation data and texture

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            // Animation update here
        }

        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
            // Could do frustum culling here if we wanted to be more efficient.
            // We could go a step further and gather the entities within sections of an octree that are
            // currently within the view frustum, then skip the frustum check here.

            //spriteBatch.Draw(Texture, Frame, Color.White); 
            // Draw will not be done here.  I am leaving the erroneous code so that I must come
            // back to this line of code.  Drawing will be done in RenderManager.  RenderManager
            // will get the texture name and source rectangle from this RenderComponent object
            // and then calculate a Frame based on the BaseEntity's XY relative to the camera.

            // NO REFERENCE TO SPRITEBATCH IN THIS CLASS.  Only need to give RenderManager the Frame
            // and the Texture name.  Maybe the CurrentFrame number for the sprite sheet?  RenderManager
            // should probably take the CurrentFrame number and Texture name and handle parsing the XML
            // for the Frame info itself.  So no Frame needed?  Only CurrentFrame number?
        //}

        protected override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
