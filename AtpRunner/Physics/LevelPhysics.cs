using AtpRunner.Components;
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

        private bool DetectCollisions()
        {
            var gameOver = false;

            // Get player
            // These Linq statements could be replaced by implementing methods
            // such as "GetPhysics", "GetRender", etc.
            var player = Scene.GetPlayer();
            var playerPhysics = (PhysicsComponent)player.Components.FirstOrDefault(n => n.Name == "Physics");
            var playerHitbox = new Rectangle(player.X, (int)player.Y, playerPhysics.Hitbox.X, playerPhysics.Hitbox.Y + 1);

            // Get obstacles
            var obstacles = Scene.GetObstacles();

            bool movingDown = player.PreviousY < player.Y;
            bool movingUp = player.PreviousY > player.Y;

            foreach (var obstacle in obstacles)
            {
                var obstaclePhysics = (PhysicsComponent)obstacle.Components.FirstOrDefault(n => n.Name == "Physics");
                var obstacleHitbox = new Rectangle(obstacle.X, (int)obstacle.Y,
                    obstaclePhysics.Hitbox.Y, obstaclePhysics.Hitbox.X);

                if (playerHitbox.Intersects(obstacleHitbox))
                {
                    // The sandwich testing has to be >= instead of >, etc. because otherwise, if the player
                    // hitbox bottom matched the obstacle bottom and the player hitbox top matched the 
                    // obstacle top, the player would pass cleanly through the obstacle.
                    bool playerTopSandwiched =
                        (obstacleHitbox.Bottom >= playerHitbox.Top && playerHitbox.Top >= obstacleHitbox.Top);

                    bool playerBottomSandwiched =
                        (obstacleHitbox.Bottom >= playerHitbox.Bottom && playerHitbox.Bottom >= obstacleHitbox.Top);

                    if (playerTopSandwiched && playerBottomSandwiched)
                    {
                        if (movingUp)
                        {
                            player.Y = obstacleHitbox.Bottom;
                        }
                        else
                        {
                            player.Y = obstacleHitbox.Top + playerHitbox.Height;
                        }
                    }
                    else if (playerTopSandwiched)
                    {
                        if (playerHitbox.Right - obstacleHitbox.Left >= obstacleHitbox.Bottom - playerHitbox.Top)
                        {
                            player.Y = obstacleHitbox.Bottom;
                        }
                        else
                        {
                            gameOver = true;
                        }
                    }
                    else if (playerBottomSandwiched)
                    {
                        if (playerHitbox.Right - obstacleHitbox.Left >= playerHitbox.Bottom - obstacleHitbox.Top)
                        {
                            player.Y = obstacleHitbox.Top - playerHitbox.Height;
                            OnTouchedGround();
                        }
                        else
                        {
                            gameOver = true;
                        }
                    }
                }
                else
                {
                    OnIsAirborneEvent();
                }


            }

            return gameOver;
        }

        protected virtual void OnTouchedGround()
        {
            if (TouchedGround != null)
            {
                TouchedGround(this, EventArgs.Empty);
            }
        }

        protected virtual void OnIsAirborneEvent()
        {
            if (IsAirborneEvent != null)
            {
                IsAirborneEvent(this, EventArgs.Empty);
            }
        }

        protected virtual void OnGetAirJump()
        {

        }
    }
}
