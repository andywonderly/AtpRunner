using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using AtpRunner.Render;

namespace AtpRunner.Components
{

    public class WinGuyInputComponent : BaseComponent
    {
        private BaseEntity _parentEntity;
        private int _speed;
        public int VelocityY { get; private set; }
        private int _velocityYMax;
        private int _gravity;

        private ContactState _contactState;
        private ContactState _previousContactState;
        public JumpState _jumpState;
        private JumpState _previousJumpState;

        private KeyboardState _previousKeyboardState;

        private int _jumpCounter;
        private int _maxJump;
        private int _minJump;

        public WinGuyInputComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _parentEntity = parentEntity;
            Name = "WinGuyInput";
          
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
            var winGuyRender = (RenderComponent)_parentEntity.Components.FirstOrDefault(n => n.Name == "Render");

            if (winGuyRender.Dimensions.X < 3000)
            {
                var newX = winGuyRender.Dimensions.X + 1;
                var newY = winGuyRender.Dimensions.Y + 1;

                winGuyRender.Dimensions = new Point(newX, newY);
            }

        }

      


    }
}
