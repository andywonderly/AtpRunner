using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;
using Microsoft.Xna.Framework;

namespace AtpRunner.Components
{
    public class PhysicsComponent : BaseComponent
    {
        public Point Hitbox { get; set; }
        public PhysicsComponent(BaseEntity parentEntity, int width, int height) : base(parentEntity)
        {
            Hitbox = new Point(height, width);
            Initialize();
            Name = "Physics";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override string GetName()
        {
            return "Physics";
        }
    }
}
