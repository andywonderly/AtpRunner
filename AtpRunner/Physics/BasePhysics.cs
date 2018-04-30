using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpRunner.Physics
{
    public abstract class BasePhysics
    {
        public Scene.Scene Scene { get; set; }
        public BasePhysics(AtpRunner.Scene.Scene scene)
        {

        }
    }
}
