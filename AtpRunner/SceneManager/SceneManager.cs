using AtpRunner.Components;
using AtpRunner.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Scene
{
    public class SceneManager : BaseManager
    {
        private MainGame mainGame;
        public List<BaseEntity> Entities { get; set; }
        public ContentManager Content { get; set;}
        public Point Camera { get; set; }
        public override string Name { get; set; }
        public KeyboardState KeyboardState { get; private set; }
        public SceneNavigation SceneNavigation { get; private set; }

        const int GAMESPEED = 4;

        public SceneManager(MainGame Game) : base(Game)
        {
            mainGame = Game;
            Entities = new List<BaseEntity>();
            Name = "Scene";
            Camera = new Point(0, 0);
            SceneNavigation = new SceneNavigation(this);
            Initialize();
        }

        protected override void Initialize()
        {
            SceneNavigation.Update();
            base.Initialize();
        }

        public override void LoadContent()
        {
            //Add player, obstactles, props to scene
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            // Get obstacles, or just obstacles near player or on screen
            // ONLY SEND OBSTACLES WITH HITBOXES
            //ProximalObstacles = Entities.Where()

            foreach(BaseEntity entity in Entities)
            {
               entity.Update(gameTime);
            }

            bool gameOver = DetectCollisions();

            if(gameOver)
            {
                mainGame.Exit();
            }

            var newX = Camera.X + GAMESPEED;
            Camera = new Point(newX, Camera.Y);
        }

        public void AddEntityToScene(BaseEntity entity)
        {
            if(Entities.Any(e => e.Name == entity.Name))
            {
                throw new Exception("An entity named " + entity.Name + " already exists in this scene.");
            }

            Entities.Add(entity);
        }

        public void RemoveEntityFromScene(BaseEntity entity)
        {
            if (!Entities.Any(e => e.Name == entity.Name))
            {
                throw new Exception("No entity named " + entity.Name + " exist in the scene to be removed.");
            }

            Entities.Remove(entity);
        }

        public BaseEntity GetEntity(string entityName)
        {
            BaseEntity entity = null;
            if (Entities.Any(e => e.Name == entityName))
            {
                entity = Entities.FirstOrDefault(e => e.Name == entityName);
            }
            else
            {
                throw new Exception("Entity named " + entityName + " was not found in this scene.");
            }

            return entity;
        }

        public List<BaseEntity> GetEntitiesWithSprites()
        {
            var entities = Entities.Where(n => n.Components.Any(c => c.Name == "Render")).ToList();

            return entities;
        }

        public List<BaseEntity> GetObstacles()
        {
            var obstacles = Entities.Where(n => n.Name == "Obstacle").ToList();

            return obstacles;
        }

        private bool DetectCollisions()
        {
            var gameOver = false;

            // Get player
            // These Linq statements could be replaced by implementing methods
            // such as "GetPhysics", "GetRender", etc.
            var player = Entities.FirstOrDefault(n => n.Name == "Player");
            var playerPhysics = (PhysicsComponent)player.Components.FirstOrDefault(n => n.Name == "Physics");
            var playerHitbox = new Rectangle(player.X, player.Y, playerPhysics.Hitbox.X, playerPhysics.Hitbox.Y);

            // Get obstacles
            var obstacles = GetObstacles();

            foreach(var obstacle in obstacles)
            {
                var obstaclePhysics = (PhysicsComponent)obstacle.Components.FirstOrDefault(n => n.Name == "Physics");
                var obstacleHitbox = new Rectangle(obstacle.X, obstacle.Y, 
                    obstaclePhysics.Hitbox.Y, obstaclePhysics.Hitbox.X);

                if(playerHitbox.Intersects(obstacleHitbox))
                {
                    // The sandwich testing has to be >= instead of >, etc. because otherwise, if the player
                    // hitbox bottom matched the obstacle bottom and the player hitbox top matched the 
                    // obstacle top, the player would pass cleanly through the obstacle.
                    bool playerTopSandwiched = 
                        (obstacleHitbox.Bottom >= playerHitbox.Top && playerHitbox.Top >= obstacleHitbox.Top);

                    bool playerBottomSandwiched = 
                        (obstacleHitbox.Bottom >= playerHitbox.Bottom && playerHitbox.Bottom >= obstacleHitbox.Top);

                    if(playerTopSandwiched && playerBottomSandwiched)
                    {
                        gameOver = true;
                    }
                    else if(playerTopSandwiched)
                    {
                        if(playerHitbox.Right - obstacleHitbox.Left >= obstacleHitbox.Bottom - playerHitbox.Top)
                        {
                            player.Y = obstacleHitbox.Bottom;
                        }
                        else
                        {
                            gameOver = true;
                        }
                    }
                    else if(playerBottomSandwiched)
                    {
                        if(playerHitbox.Right - obstacleHitbox.Left >= playerHitbox.Bottom - obstacleHitbox.Top)
                        {
                            player.Y = obstacleHitbox.Top - playerHitbox.Height;
                        }
                        else
                        {
                            gameOver = true;
                        }
                    }
                    
                }
            }

            return gameOver;
        }
    }
}
