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
    public class LevelPhysics : BasePhysics
    {
        public LevelPhysics(Scene.Scene scene) : base(scene)
        {
            Scene = scene;
        }

        public override void Update(List<BaseEntity> entities)
        {
            BaseEntity player = Scene.GetPlayer();
            List<BaseEntity> obstacles = Scene.GetObstacles();
            List<BaseEntity> platforms = Scene.GetPlatforms();
            DetectPlatformCollisions(player, platforms);
            DetectObstacleCollisions(player, obstacles);
        }

        private void DetectPlatformCollisions(BaseEntity player, List<BaseEntity> platforms)
        {
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
                    if(movingUp)
                    {
                        player.Y = platformHitbox.Bottom;
                         
                        // Send message to end jump and begin descent
                    }
                    else if(movingDown)
                    {
                        // If bottom of player is within a step (one speed unit, or 4 pixels)
                        // then rest on the platform.

                        if(player.Y - platformHitbox.Bottom < 5)
                        {
                            player.Y = platformHitbox.Top + playerHitbox.Height;

                            // And send message to set player state to grounded if need be
                        }
                    }
                }
            }
        }

        private void DetectObstacleCollisions(BaseEntity player, List<BaseEntity> obstacles)
        {
            var playerPhysics = (PhysicsComponent)player.Components.FirstOrDefault(n => n.Name == "Physics");
            var playerHitbox = new Rectangle(player.X, (int)player.Y, playerPhysics.Hitbox.X, playerPhysics.Hitbox.Y);

            bool movingDown = player.PreviousY < player.Y;
            bool movingUp = player.PreviousY > player.Y;

            foreach (var obstacle in obstacles)
            {
                var obstaclePhysics = (PhysicsComponent)obstacle.Components.FirstOrDefault(n => n.Name == "Physics");
                var obstacleHitbox = new Rectangle(obstacle.X, (int)obstacle.Y,
                    obstaclePhysics.Hitbox.Y, obstaclePhysics.Hitbox.X);

                if (playerHitbox.Intersects(obstacleHitbox))
                {
                    // Game over message
                }
            }
        }


    }
}
