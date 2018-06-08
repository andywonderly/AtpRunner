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
    public class MainMenuInputComponent : BaseComponent
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

        private int _jumpCounter;
        private int _maxJump;
        private int _minJump;

        public MainMenuInputComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _parentEntity = parentEntity;
            Name = "Input";
            _speed = 4;
            _gravity = 1;
            _velocityYMax = 8;
            _contactState = ContactState.Airborne;
            _previousContactState = ContactState.Airborne;
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
            _previousContactState = _contactState; 
            _parentEntity.PreviousX = _parentEntity.X;
            _parentEntity.X += _speed;
            _parentEntity.Y += VelocityY;

            VelocityY += _gravity;

            if(VelocityY > _velocityYMax)
            {
                VelocityY = _velocityYMax;
            }


        }

        public void DoubleJump()
        {
            _jumpState = JumpState.CanJump;
            
        }

        public void AutoJump()
        {
            Jump();
        }
        private void Jump()
        {
            if(_jumpState == JumpState.CanJump)
            {
                VelocityY = -15;
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
