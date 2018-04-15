using AtpRunner.Entities;
using AtpRunner.Render;
using AtpRunner.SceneLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Scene
{
    public class SceneNavigation
    {
        // Scene navigation is intended to be a road map for the game.  So it determines when to load
        // things like a splash screen, main menu, and then levels, as appropriate.
        private bool _level1Started;
        private bool _level1Finished;
        public SceneManager Scene { get; private set; }
        public const int CAMSTARTX = 0;
        public const int CAMSTARTY = 0;
        public const int PLAYERSTARTX = 10;
        public const int PLAYERSTARTY = 100;

        public SceneNavigation(SceneManager scene)
        {
            Scene = scene;
            _level1Started = false;
            _level1Finished = false;
        }

        public void Update()
        {
            if(!_level1Started && !_level1Finished)
            {
                var level1 = new Level1(Scene);
                Scene.Entities = new List<BaseEntity>();

                var player = level1.BuildPlayer(PLAYERSTARTX, PLAYERSTARTY);

                Scene.Camera = new Point(CAMSTARTX, CAMSTARTY);

                var obstacles = level1.BuildObstacles();

                Scene.Entities.Add(player);

                foreach(BaseEntity obstacle in obstacles)
                {
                    Scene.Entities.Add(obstacle);
                }
                

                _level1Started = true;
            }
        }
    }
}
