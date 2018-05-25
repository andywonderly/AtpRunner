using AtpRunner.Entities;
using AtpRunner.Menu;
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
        protected List<BaseEntity> Entities;
        public Point Camera;
        bool MenuActive;
        public BasePhysics Physics;
        BaseMenu Menu;

        public Scene()
        {
            Entities = new List<BaseEntity>();
            Camera = new Point(-40, 180);
        }

        public void Update(GameTime gameTime)
        {
            
            KeyboardState = Keyboard.GetState();
            Camera.X += 4;
            
            if(KeyboardState.IsKeyDown(Keys.Escape))
            {
                MenuActive = true;
            }

            if (!MenuActive)
            {
                UpdateEntities(gameTime);
            }
            else
            {
                UpdateMenu();
            }

            UpdatePhysics();
        }
        public void UpdatePhysics()
        {
            Physics.Update(Entities); // Arguments??
        }

        public void UpdateMenu()
        {
            Menu.Update(); // Lots of stuff needed.
        }

        private void UpdateEntities(GameTime gameTime)
        {
            foreach(BaseEntity entity in Entities)
            {
                entity.Update(gameTime);
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
            var obstacles = Entities.Where(n => n.Name.Contains("Obstacle") || n.Name.Contains("DoubleJump")).ToList();

            return obstacles;
        }

        public List<BaseEntity> GetPlatforms()
        {
            var platforms = Entities.Where(n => n.Name.Contains("Platform")).ToList();

            return platforms;
        }

        public virtual BaseEntity GetPlayer()
        {
            return Entities.FirstOrDefault(n => n.Name == "Player");
        }

    }
}
