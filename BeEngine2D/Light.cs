using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class Light
    {
        public Light()
        {

        }

        public Light(Vector2D Position, float Range, float Intensity)
        {
            this.Position = Position;
            this.Range = Range;
            this.Intensity = Intensity;

            BeEngine2D.RegisterLight(this);
        }

        public int ObjectID { get; }
        public float Range { get; set; }
        public float Intensity { get; set; }
        public Vector2D Position { get; set; }
    }
}
