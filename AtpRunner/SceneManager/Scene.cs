using AtpRunner.Entities;
using AtpRunner.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Scene
{
    public class Scene
    {
        public KeyboardState KeyboardState;
        BasePhysics LevelPhysics;
        protected List<BaseEntity> Entities;
        Point Camera;
        GameTime GameTime;
        bool MenuActive;
        BasePhysics Physics;

        public delegate void OnTouchedGroundEventHandler(object sender, EventArgs e);
        public event OnTouchedGroundEventHandler TouchedGround;
        public delegate void OnGetAirJumpEventHandler(object sender, EventArgs e);
        public event OnGetAirJumpEventHandler GotAirJump;
        public delegate void OnMenuRequestedEventHandler(object sender, EventArgs e);
        public event OnMenuRequestedEventHandler MenuRequested;

        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Camera = new Point(0, 0);
            KeyboardState = Keyboard.GetState();
            UpdatePhysics();
            if (!MenuActive)
            {
                UpdateEntities();
            }
            else
            {
                UpdateMenu();
            }
        }
        public void UpdatePhysics()
        {
            Physics.Update(); // Arguments??
        }

        public void UpdateMenu()
        {
            Menu.Update(); // Lots of stuff needed.
        }
        private void UpdateEntities()
        {
            foreach(BaseEntity entity in Entities)
            {
                entity.Update(GameTime);
            }
        }
        public void AddEntityToScene(BaseEntity entity)
        {
            if (Entities.Any(e => e.Name == entity.Name))
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

        public virtual BaseEntity GetPlayer()
        {
            return Entities.FirstOrDefault(n => n.Name == "Player");
        }

    }
}
