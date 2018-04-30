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
        private float _velocityY;
        private float _gravity;
        private bool _airJump;
        private bool _jumpingLastFrame;
        private PlayerState _playerState;
        private PlayerState _previousState;

        private enum PlayerState
        {
            Grounded,
            Airborne,
        }

        public InputComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _parentEntity = parentEntity;
            Name = "Input";
            _speed = 4;
            _velocityY = 0f;
            _gravity = 0.5f;
            _airJump = false;
            _jumpingLastFrame = false;

            _playerState = PlayerState.Airborne;
            _previousState = PlayerState.Airborne;
            
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
            _previousState = _playerState;

            _parentEntity.PreviousY = _parentEntity.Y;

            if(keyboardState.IsKeyDown(Keys.Up))
            {
                Jump();
            }
            else if(_jumpingLastFrame == true)
            {
                EndJump();
            }

            if(_playerState == PlayerState.Grounded)
            {
                _velocityY = 0;
            }
            else
            {
                _velocityY += _gravity;
                _parentEntity.PreviousY = _parentEntity.Y;
                _parentEntity.Y += _velocityY;
            }
            _parentEntity.PreviousX = _parentEntity.X;
            _parentEntity.X += _speed;

            if(_velocityY > 10)
            {
                _velocityY = 10f;
            }
        }



        private void OnJumpPowerup(object sender, EventArgs e)
        {
            _airJump = true;
        }

        private void Jump()
        {
            if(_playerState == PlayerState.Grounded || _airJump)
            {
                _velocityY = -12.0f;
                _playerState = PlayerState.Airborne;

                if(_airJump)
                {
                    _airJump = false;
                }
            }

            _jumpingLastFrame = true;
        }

        private void EndJump()
        {
            if(_velocityY < -6.0)
            {
                _velocityY = -6.0f;
            }

            _jumpingLastFrame = false;
        }

        public void OnTouchedGround(object sender, EventArgs e)
        {
            if (_playerState == PlayerState.Airborne)
            {
                _playerState = PlayerState.Grounded;
                _velocityY = 0;
                _airJump = false;
                _jumpingLastFrame = false;
            }
        }

        public void OnAirborne(object sender, EventArgs e)
        {
            _playerState = PlayerState.Airborne;
        }
    }
}
