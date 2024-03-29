﻿using AtpRunner.Components;
using AtpRunner.Entities;
using AtpRunner.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Physics
{
    public class LevelPhysics : BasePhysics
    {
        private JumpSoundEffectPlayer JumpSound;
        public LevelPhysics(Scene.Scene scene) : base(scene)
        {
            Scene = scene;
            JumpSound = new JumpSoundEffectPlayer(scene);
        }

        public override void Update(List<BaseEntity> entities)
        {
            BaseEntity player = Scene.GetPlayer();

            if(player.X > 5280)
            {
                Scene.SceneManager.LevelComplete();
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

            if(player.Y > 1200)
            {
                Scene.SceneManager.ReloadLevel();
                var comeOn = Scene.SceneManager.MainGame.Content.Load<SoundEffect>("ComeOnMan");
                comeOn.Play();
            }

            JumpSound.Update();
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

                    if(obstacle.Name.Contains("DoubleJump"))
                    {
                        DoubleJump();
                        JumpSound.Play();
                    }

                    if(obstacle.Name.Contains("Obstacle"))
                    {
                        var buttHash = Scene.SceneManager.MainGame.Content.Load<SoundEffect>("ButtHash");
                        buttHash.Play();
                        Scene.SceneManager.ReloadLevel();
                    }

                    // Removing the entity is a simple way to get rid of it.  I may want to change its color
                    // or something fancy like that.  For now, I remove it because I was experiencing the following
                    // issue:  You would intermittently be able to triple jump after contacting a double jump.  This
                    // happened if you used your double jump while you were still in contact with the powerup because
                    // you would continualy be awarded a double jump every frame that you're in contact with it.
                    // So removing it prevents you from being awarded a double jump for every frame that you're
                    // in contact with it.  Now, you'll only get a double jump once and then the powerup disappears
                    // so you can't touch it again.

                    Scene.RemoveEntityFromScene(obstacle);
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

public class JumpSoundEffectPlayer
{
    private SoundEffect Rise01;
    private SoundEffect Rise02;
    private SoundEffect Rise03;

    private bool play1;
    private bool play2;
    private bool play3;

    private int counter;

    public JumpSoundEffectPlayer(Scene scene)
    {
        Rise01 = scene.SceneManager.MainGame.Content.Load<SoundEffect>("Rise01");
        Rise02 = scene.SceneManager.MainGame.Content.Load<SoundEffect>("Rise02");
        Rise03 = scene.SceneManager.MainGame.Content.Load<SoundEffect>("Rise03");

        play1 = false;
        play2 = false;

        counter = 0;
    }

    public void Update()
    {
        if(play1 || play2 || play3)
        {
            counter++;

            if(counter > 60)
            {
                play1 = false;
                play2 = false;
                play3 = false;

                counter = 0;
            }
        }
    }

    public void Play()
    {
        if (!play1 && !play2)
        {
            Rise01.Play();
            play1 = true;
            counter = 0;
        }
        else if (play1 && !play2)
        {
            Rise02.Play();
            play2 = true;
            counter = 0;
        }
        else if(play1 && play2)
        {
            Rise03.Play();
            counter = 0;
        }
    }

}
