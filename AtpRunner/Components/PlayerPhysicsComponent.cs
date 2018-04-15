using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;

namespace AtpRunner.Components
{
    public class PlayerPhysicsComponent : BaseComponent
    {
        public Rectangle Hitbox { get; private set; }
        private List<BaseEntity> _obstacles;
        private BaseEntity _parentEntity;

        public PlayerPhysicsComponent(BaseEntity parentEntity) : base(parentEntity)
        {
            _obstacles = new List<BaseEntity>();
            _parentEntity = parentEntity;
            //Hitbox = new Rectangle(123123 /*placeholder*/);
        }

        protected override string GetName()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            //_obstacles = _parentEntity.Manager.ProximalObstacles;

            //foreach(var obstacle in _obstacles)
            //{
            //    //Get physics component of obstacle
            //    PlayerPhysicsComponent obstaclePhysics = obstacle.GetComponent("Physics") as PlayerPhysicsComponent;
            //    if (obstaclePhysics != null)
            //    {
            //        if (Hitbox.Intersects(obstaclePhysics.Hitbox))
            //        {

            //        }
            //    }
            //}
        }

    }
}
