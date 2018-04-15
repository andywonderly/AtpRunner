using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AtpRunner.Entities;
using AtpRunner.Render;
using Microsoft.Xna.Framework.Graphics;

namespace AtpRunner.Components
{
    abstract public class BaseComponent
    {
        public BaseEntity Parent { get; private set; }

        public string Name { get; set; }

        public BaseComponent(BaseEntity parentEntity)
        {
            Parent = parentEntity;
        }

        // Name of the component
        protected abstract string GetName();

        protected virtual void Initialize()
        {
            Parent.AddComponent(this);
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
