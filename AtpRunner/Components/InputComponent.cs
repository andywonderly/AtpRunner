using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AtpRunner.Components
{
    public class InputComponent : BaseComponent
    {
        private BaseEntity _parentEntity;
        private int _speed;
        public InputComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _parentEntity = parentEntity;
            Name = "Input";
            _speed = 4;
            Initialize();
        }

        protected override string GetName()
        {
            return Name;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = _parentEntity.Manager.KeyboardState;

            if(keyboardState.IsKeyDown(Keys.Up))
            {
                _parentEntity.Y -= _speed;
            }

            if(keyboardState.IsKeyDown(Keys.Down))
            {
                _parentEntity.Y += _speed;
            }

            _parentEntity.X += _speed;
        }
    }
}
