using AtpRunner.Components;
using AtpRunner.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Physics
{
    public class MainMenuLevelPhysics : BasePhysics
    {
        public MainMenuLevelPhysics(Scene.Scene scene) : base(scene)
        {
            Scene = scene;
        }

        public override void Update(List<BaseEntity> entities)
        {
            BaseEntity player = Scene.GetPlayer();

            if(player.X > 800)
            {
                Scene.Camera = new Point(-40, 180);
                player.X = 0;
            }

            List<BaseEntity> obstacles = Scene.GetObstacles();
            List<BaseEntity> platforms = Scene.GetPlatforms();
            
            bool hitPlatform = DetectPlatformCollisions(player, platforms);
            bool hitObstacle = DetectObstacleCollisions(player, obstacles);

            if(!(hitPlatform || hitObstacle))
            {
                FreeFall();
            }

            player.PreviousY = player.Y;
        }

        private void FreeFall()
        {
            BaseEntity player = Scene.GetPlayer();
            InputComponent input = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");

            input.FreeFall();
        }

        private bool DetectPlatformCollisions(BaseEntity player, List<BaseEntity> platforms)
        {
            bool collision = false;

            var playerPhysics = (PhysicsComponent)player.Components.FirstOrDefault(n => n.Name == "Physics");
            var playerHitbox = new Rectangle(player.X, (int)player.Y, playerPhysics.Hitbox.X, playerPhysics.Hitbox.Y + 1);

            bool movingDown = player.PreviousY < player.Y;
            bool movingUp = player.PreviousY > player.Y;

            foreach (var platform in platforms)
            {
                var platformPhysics = (PhysicsComponent)platform.Components.FirstOrDefault(n => n.Name == "Physics");
                var platformHitbox = new Rectangle(platform.X, (int)platform.Y,
                    platformPhysics.Hitbox.Y, platformPhysics.Hitbox.X);

                if (playerHitbox.Intersects(platformHitbox))
                {
                    collision = true;

                    if(movingUp)
                    {
                        //player.Y = platformHitbox.Bottom;
                         
                        // Send message to end jump and begin descent
                    }
                    else if(movingDown)
                    {
                        // If bottom of player is within a step (one speed unit, or 4 pixels)
                        // then rest on the platform.

                        

                        var input = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");

                        if(playerHitbox.Bottom - platformHitbox.Bottom < input.VelocityY + 1)
                        {
                            player.Y = platformHitbox.Top - playerHitbox.Height + 1;

                            // And send message to set player state to grounded if need be

                            PlayerTouchedDown();
                        }
                    }
                }
            }

            return collision;
        }

        private bool DetectObstacleCollisions(BaseEntity player, List<BaseEntity> obstacles)
        {
            bool collision = false;

            var playerPhysics = (PhysicsComponent)player.Components.FirstOrDefault(n => n.Name == "Physics");
            var playerHitbox = new Rectangle(player.X, (int)player.Y, playerPhysics.Hitbox.X, playerPhysics.Hitbox.Y);
            var playerInput = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");

            bool movingDown = player.PreviousY < player.Y;
            bool movingUp = player.PreviousY > player.Y;

            foreach (var obstacle in obstacles)
            {
                var obstaclePhysics = (PhysicsComponent)obstacle.Components.FirstOrDefault(n => n.Name == "Physics");
                var obstacleHitbox = new Rectangle(obstacle.X, (int)obstacle.Y,
                    obstaclePhysics.Hitbox.Y, obstaclePhysics.Hitbox.X);

                if (playerHitbox.Intersects(obstacleHitbox))
                {
                     collision = true;

                    if(obstacle.Name.Contains("AutoJump"))
                    {
                        playerInput.AutoJump();
                    }
                }
            }

            return collision;
        }

        private void PlayerTouchedDown()
        {
            BaseEntity player = Scene.GetPlayer();
            InputComponent input = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");

            input.TouchedGround();
        }

        private void DoubleJump()
        {
            BaseEntity player = Scene.GetPlayer();
            InputComponent input = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");

            input.DoubleJump();
        }


    }
}
