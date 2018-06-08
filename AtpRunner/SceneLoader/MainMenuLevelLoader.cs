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
    class MainMenuLevelLoader : BaseSceneLoader
    {
        public override int PLAYERSTARTX { get; set; }

        public override int PLAYERSTARTY { get; set; }
        public string LevelName { get; set; }

        public MainMenuLevelLoader(SceneManager sceneManager, AtpRunner.Scene.Scene scene, string levelName) : base()
        {
            PLAYERSTARTX = 0;
            PLAYERSTARTY = 300;

            LevelName = levelName;
            SceneManager = sceneManager;
            Scene = scene;

            var physics = LoadPhysics();
            Scene.Physics = physics;

            Initialize();
            
        }

        protected override void Initialize()
        {
            var player = BuildPlayer(PLAYERSTARTX, PLAYERSTARTY);
            Scene.AddEntityToScene(player);

            LoadEntities();

            base.Initialize();
        }
        public override BaseEntity BuildPlayer(int startX, int startY)
        {
            var player = new BaseEntity(SceneManager, "Player", startX, startY);

            var renderComponent = new RenderComponent(player, "atpRtsd4", 32, 32);

            var inputComponent = new MainMenuInputComponent(player);

            var physicsComponent = new PhysicsComponent(player, 32, 32);

            return player;
        }

        public void LoadEntities()
        {
            for(var i = -6; i < 100; i++)
            {
                var platformEntity = new BaseEntity(SceneManager, "Platform" + i.ToString(), i * 32, 480);
                var platformRender = new RenderComponent(platformEntity, "Platform", 32, 32);
                var platformPhysics = new PhysicsComponent(platformEntity, 32, 32);

                Scene.AddEntityToScene(platformEntity);
            }

            

            var obstacle1 = new BaseEntity(SceneManager, "Obstacle1", 1080, 480 - 32);
            var obstacle1render = new RenderComponent(obstacle1, "atpButtHash", 32, 32);
            var obstacle1physics = new PhysicsComponent(obstacle1, 32, 32);

            Scene.AddEntityToScene(obstacle1);

            var obstacle2 = new BaseEntity(SceneManager, "Obstacle2", 1080, 480 - 64);
            var obstacle2render = new RenderComponent(obstacle2, "atpButtHash", 32, 32);
            var obstacle2physics = new PhysicsComponent(obstacle2, 32, 32);

            Scene.AddEntityToScene(obstacle2);

            var autoJump1 = new BaseEntity(SceneManager, "AutoJump1", 1032, 480 - 32);
            var autoJump1physics = new PhysicsComponent(autoJump1, 32, 32);

            Scene.AddEntityToScene(autoJump1);

            for (var i = 0; i < 6; i++)
            {
                var obstacle = new BaseEntity(SceneManager, "Obstacle" + (i + 3).ToString(), 1700, 480 - 32 * i);
                var obstacleRender = new RenderComponent(obstacle, "atpButtHash", 32, 32);
                var obstaclePhysics = new PhysicsComponent(obstacle, 32, 32);

                Scene.AddEntityToScene(obstacle);
            }

            var autoJump2 = new BaseEntity(SceneManager, "AutoJump2", 1604, 480 - 32);
            //var autoJump2render = new RenderComponent(autoJump2, "atpGood", 32, 32);
            var autoJump2physics = new PhysicsComponent(autoJump2, 32, 32);

            Scene.AddEntityToScene(autoJump2);

            var autoJump3 = new BaseEntity(SceneManager, "AutoJump3", 1670, 480 - 115);
            //var autoJump3render = new RenderComponent(autoJump3, "atpGood", 32, 32);
            var autoJump3physics = new PhysicsComponent(autoJump3, 32, 32);

            Scene.AddEntityToScene(autoJump3);

            var doubleJump1 = new BaseEntity(SceneManager, "DoubleJump1", 1660, 480 - 80 - 64);
            var doubleJump1render = new RenderComponent(doubleJump1, "atpSolid", 32, 32);
            var doubleJump1physics = new PhysicsComponent(doubleJump1, 32, 32);

            Scene.AddEntityToScene(doubleJump1);

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

                var renderComponent = new RenderComponent(obstacle, "atpButtHash", 32, 32);
                var physicsComponent = new PhysicsComponent(obstacle, 32, 32);
                obstacles.Add(obstacle);

            }

            var ground = new BaseEntity(SceneManager, "Platform1", 0, 400);
            var groundRender = new RenderComponent(ground, "BlackDot", 100000, 32);
            var groundPhysics = new PhysicsComponent(ground, 100000, 10);

            obstacles.Add(ground);
            
            return obstacles;
        }

        public override BasePhysics LoadPhysics()
        {
            return new MainMenuLevelPhysics(Scene);
        }
    }
}
