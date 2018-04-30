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

namespace AtpRunner.SceneLoader
{
    class Level1Loader : SceneLoader
    {
        public override int PLAYERSTARTX { get; set; }

        public override int PLAYERSTARTY { get; set; }

        public Level1Loader(SceneManager sceneManager, AtpRunner.Scene.Scene scene) : base()
        {
            PLAYERSTARTX = 0;
            PLAYERSTARTY = 100;

            SceneManager = sceneManager;
            Scene = scene;
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
                var obstacle = new BaseEntity(SceneManager, "Obstacle", startX, startY);

                if(i % 2 == 0)
                {
                    startY -= oscillation;
                }
                else
                {
                    startY += oscillation;
                }

                startX += 150;

                var renderComponent = new RenderComponent(obstacle, "GreenDot", 32, 32);
                var physicsComponent = new PhysicsComponent(obstacle, 32, 32);

                obstacles.Add(obstacle);

            }

            var ground = new BaseEntity(SceneManager, "Obstacle", 0, 400);
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
            return new LevelPhysics();
        }
    }
}
