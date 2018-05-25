using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;
using AtpRunner.Scene;
using AtpRunner.Render;
using AtpRunner.Components;
using AtpRunner.Physics;
using static LevelParser.AtpLevelParser;

namespace AtpRunner.SceneLoader
{
    class Level1Loader : BaseSceneLoader
    {
        public override int PLAYERSTARTX { get; set; }

        public override int PLAYERSTARTY { get; set; }

        public Level1Loader(SceneManager sceneManager, AtpRunner.Scene.Scene scene) : base()
        {
            PLAYERSTARTX = 0;
            PLAYERSTARTY = 300;

            SceneManager = sceneManager;
            Scene = scene;
            Initialize();
            
        }

        protected override void Initialize()
        {
            var player = BuildPlayer(PLAYERSTARTX, PLAYERSTARTY);
            Scene.AddEntityToScene(player);

            //var obstacles = BuildObstacles();

            TiledData level = Parse();

            var objectLayer = level.layers.FirstOrDefault(n => n.name == "Object Layer 1");
            var objects = objectLayer.objects;

            var obstacles = objects.Where(n => n.gid == 15);
            var platforms = objects.Where(n => n.gid == 5);
            //var platforms = new List<TiledObject>();
            var doubleJumps = objects.Where(n => n.gid == 9);

            var i = 0;
            foreach(var obstacle in obstacles)
            {
                var obstacleEntity = new BaseEntity(SceneManager, "Obstacle" + i.ToString(), obstacle.x, obstacle.y);

                var renderComponent = new RenderComponent(obstacleEntity, "MaroonDot", obstacle.width, obstacle.height);
                var physicsComponent = new PhysicsComponent(obstacleEntity, obstacle.width, obstacle.height);
                
                Scene.AddEntityToScene(obstacleEntity);

                i++;
            }

            i = 0;
            foreach(var platform in platforms)
            {
                var platformEntity = new BaseEntity(SceneManager, "Platform" + i.ToString(), platform.x, platform.y);

                var groundRender = new RenderComponent(platformEntity, "BlackDot", platform.width, platform.height);
                var groundPhysics = new PhysicsComponent(platformEntity, platform.width, platform.height);

                Scene.AddEntityToScene(platformEntity);

                i++;
            }

            i = 0;
            foreach(var doubleJump in doubleJumps)
            {
                var doubleJumpEntity = new BaseEntity(SceneManager, "DoubleJump" + i.ToString(), 
                    doubleJump.x, doubleJump.y);

                var renderComponent = new RenderComponent(doubleJumpEntity, "GreenDot", doubleJump.width, doubleJump.height);
                var physicsComponent = new PhysicsComponent(doubleJumpEntity, doubleJump.width, doubleJump.height);

                Scene.AddEntityToScene(doubleJumpEntity);

                i++;
            }

            var physics = LoadPhysics();
            Scene.Physics = physics;

            base.Initialize();
        }
        public override BaseEntity BuildPlayer(int startX, int startY)
        {
            var player = new BaseEntity(SceneManager, "Player", startX, startY);

            var renderComponent = new RenderComponent(player, "RedDot", 32, 32);

            var inputComponent = new InputComponent(player);

            var physicsComponent = new PhysicsComponent(player, 32, 32);

            return player;
        }

        public override List<BaseEntity> BuildObstacles()
        {
            int startX = 1300;
            int startY = 300;
            var oscillation = 200;

            List<BaseEntity> obstacles = new List<BaseEntity>();

            for(var i = 0; i < 100; i++)
            {
                var name = "Obstacle";

                if(i % 2 == 0)
                {
                    startY -= oscillation;
                }
                else
                {
                    name = "DoubleJump";
                    startY += oscillation;
                }

                var obstacle = new BaseEntity(SceneManager, name + i.ToString(), startX, startY);

                startX += 150;

                var renderComponent = new RenderComponent(obstacle, "GreenDot", 32, 32);
                var physicsComponent = new PhysicsComponent(obstacle, 32, 32);
                obstacles.Add(obstacle);

            }

            var ground = new BaseEntity(SceneManager, "Platform1", 0, 400);
            var groundRender = new RenderComponent(ground, "BlackDot", 100000, 32);
            var groundPhysics = new PhysicsComponent(ground, 100000, 10);

            obstacles.Add(ground);
            
            return obstacles;
        }

        private void AddPlayerEventListeners()
        {
            var player = Scene.GetPlayer();
            var inputComponent = (InputComponent)player.Components.FirstOrDefault(n => n.Name == "Input");
        }

        public override BasePhysics LoadPhysics()
        {
            return new LevelPhysics(Scene);
        }
    }
}
