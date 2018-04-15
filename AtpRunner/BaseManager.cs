using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner
{
    abstract public class BaseManager
    {
        public MainGame Game { get; private set; }
        public abstract string Name { get; set; }

        public BaseManager(MainGame game)
        {
            Game = game;
        }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }

        protected virtual void Initialize()
        {
            Game.AddManager(this);
        }
    }
}
