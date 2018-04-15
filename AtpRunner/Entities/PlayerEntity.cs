using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Scene;
using Microsoft.Xna.Framework;

namespace AtpRunner.Entities
{
    public class PlayerEntity : BaseEntity
    {
        new string Name;
        new SceneManager Manager;
        public PlayerEntity(SceneManager sceneManager, string EntityName, int startX, int startY) 
            : base(sceneManager, EntityName, startX, startY)
        {
            Name = EntityName;
            Manager = sceneManager;
        }

        public new void Update(GameTime gameTime)
        {

        }
    }
}
