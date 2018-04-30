using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpRunner.Entities;

namespace AtpRunner.SceneLoader
{
    public class MainMenuBackground : SceneLoader
    {
        public override int PLAYERSTARTX { get; set; }

        public override int PLAYERSTARTY { get; set; }\

        public override List<BaseEntity> BuildObstacles()
        {
            throw new NotImplementedException();
        }

        public override BaseEntity BuildPlayer(int startX, int startY)
        {
            throw new NotImplementedException();
        }
    }
}
