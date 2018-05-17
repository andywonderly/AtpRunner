using AtpRunner.Components;
using AtpRunner.Entities;
using AtpRunner.Physics;
using AtpRunner.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.SceneLoader
{
    public abstract class BaseSceneLoader
    {
        // The point of LevelFactory is to be a wrapper for essentially custom methods that create level objects.
        // So you can manually create objects and add them to the scene as is done in Level1.cs, or you could
        // code a loader that reads a level file (text, JSON, etc.) and creates objects based on the data in that 
        // file.  That is what I would envision for a fully-developed game.

        public abstract int PLAYERSTARTX { get; set; }
        public abstract int PLAYERSTARTY { get; set; }

        protected SceneManager SceneManager { get; set; }
        protected Scene.Scene Scene { get; set; }
        protected BaseSceneLoader() { }
        public abstract BaseEntity BuildPlayer(int startX, int startY);
        public abstract List<BaseEntity> BuildObstacles();
        public abstract BasePhysics LoadPhysics();

        protected virtual void Initialize()
        {

        }
        
    }
}
