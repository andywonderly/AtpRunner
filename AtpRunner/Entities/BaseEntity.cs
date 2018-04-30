using AtpRunner.Components;
using AtpRunner.Scene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Render;
using Microsoft.Xna.Framework.Graphics;

namespace AtpRunner.Entities
{
    public class BaseEntity
    {
        public SceneManager Manager { get; private set; }
        public string Name { get; private set; }
        public List<BaseComponent> Components { get; private set; }
        public int X { get; set; }
        public int PreviousX { get; set; }
        public float Y { get; set; }
        public float PreviousY { get; set; }

        // Public for performance reasons, accessors cause copies
        public Point Position = new Point(0, 0);
        public float Rotation = 0f;

        public BaseEntity(SceneManager sceneManager, string EntityName, int startX, int startY)
        {
            Components = new List<BaseComponent>();
            Manager = sceneManager;
            Name = EntityName;
            X = startX;
            PreviousX = X;
            Y = startY;
            PreviousY = Y;
        }

        public void AddComponent(BaseComponent component)
        {
            if (Components.Any(c => c.Name == component.Name))
            {
                throw new Exception("Component type " + component.Name + " already exists on this object: " + Name);
            }

            Components.Add(component);
        }

        public void RemoveComponent(BaseComponent component)
        {
            if (!Components.Any(c => c.Name == component.Name))
            {
                throw new Exception("Component type " + component.Name + " doesn't exists on this object: " + Name);
            }

            Components.Remove(component);
        }

        public BaseComponent GetComponent(string componentName)
        {
            BaseComponent component = null;
            if (Components.Any(c => c.Name == componentName))
            {
                component = Components.FirstOrDefault(n => n.Name == componentName);
            }
            else
            {
                throw new Exception("Component named " + componentName + " was not found in this object: " + Name);
            }

            return component;
        }

        public void Update(GameTime gameTime)
        {
            foreach(BaseComponent component in Components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Components.Any(n => n.Name == "Render"))
            {
                var renderComponent = Components.FirstOrDefault(c => c.Name == "Render");
                renderComponent.Draw(gameTime, spriteBatch);
            }
        }
    }
}
