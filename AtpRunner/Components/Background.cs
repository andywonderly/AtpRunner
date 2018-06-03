using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Components
{
    public class Background
    {
        public Scene.Scene ParentScene { get; set; }
        public Texture2D Texture { get; set; }
        public int X { get; set; }

        public Background(Scene.Scene scene, Texture2D texture, int x)
        {
            X = x;
            Texture = texture;
            ParentScene = scene;
        }
        public void Update()
        {
            var camera = ParentScene.Camera.X;

            if (!ParentScene.MenuActive)
            {
                if (camera % 4 == 0)
                {
                    X++;
                }
            }
        }
    }
}
