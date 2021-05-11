using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D()
        {
            X = 0;
            Y = 0;
        }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2D Negate()
        {
            X = -1 * X;
            Y = -1 * Y;

            return this;
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }
    }
}
