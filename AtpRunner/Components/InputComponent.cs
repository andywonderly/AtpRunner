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
    public enum ContactState
    {
        Grounded,
        Airborne,
    }

    public enum JumpState
    {
        CanJump,
        CantJump,
    }

    public class InputComponent : BaseComponent
    {
        private BaseEntity _parentEntity;
        private int _speed;
        public int VelocityY { get; private set; }
        private int _velocityYMax;
        private int _gravity;

        private ContactState _contactState;
        private ContactState _previousContactState;
        private JumpState _jumpState;
        private JumpState _previousJumpState;

        private KeyboardState _previousKeyboardState;

        private int _jumpCounter;
        private int _maxJump;
        private int _minJump;

        public InputComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _parentEntity = parentEntity;
            Name = "Input";
            _speed = 4;
            _gravity = 1;

            _velocityYMax = 8;

            _contactState = ContactState.Airborne;
            _previousContactState = ContactState.Airborne;

            _previousKeyboardState = Keyboard.GetState();

            _jumpCounter = 0;
            _maxJump = 10;
            _minJump = 7;

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
            
            KeyboardState keyboardState = _parentEntity.Manager.Scene.KeyboardState;
            _previousContactState = _contactState;

            if(!(_previousKeyboardState.IsKeyDown(Keys.Up) || _previousKeyboardState.IsKeyDown(Keys.Space) || 
                _previousKeyboardState.IsKeyDown(Keys.W)) && (
                keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.W)))
            {
                Jump();
            }
            
            _parentEntity.PreviousX = _parentEntity.X;
            _parentEntity.X += _speed;
            _parentEntity.Y += VelocityY;

            VelocityY += _gravity;

            if(VelocityY > _velocityYMax)
            {
                VelocityY = _velocityYMax;
            }

            _previousKeyboardState = keyboardState;
        }

        public void DoubleJump()
        {
            _jumpState = JumpState.CanJump;
            
        }

        private void Jump()
        {
            if(_jumpState == JumpState.CanJump)
            {
                VelocityY = -14;
                _jumpState = JumpState.CantJump;
            }  
        }

        public void TouchedGround()
        {
            _contactState = ContactState.Grounded;
            _jumpState = JumpState.CanJump;
        }

        public void FreeFall()
        {
            _contactState = ContactState.Airborne;

            if(_previousContactState == ContactState.Grounded)
            {
                _jumpState = JumpState.CantJump;
            }
        }
    }
}
