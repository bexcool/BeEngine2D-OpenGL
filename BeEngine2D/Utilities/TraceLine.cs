using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D.Utilities
{
    public class TraceLine
    {
        public TraceLine()
        {

        }

        /// <summary>
        /// Finds position by rotation and distance.
        /// </summary>
        /// <param name="Position">Original position.</param>
        /// <param name="RotationInDegrees">Rotate new vector in degrees.</param>
        /// <param name="Distance">Distance from original position.</param>
        /// <returns>New position.</returns>
        public static Vector2 ShootLineTrace(Vector2 Position, float RotationInDegrees, float Distance)
        {
            return new Vector2
            {
                X = (float)Math.Sin(Math.PI / 180 * RotationInDegrees) * Distance + Position.X,
                Y = (float)Math.Cos(Math.PI / 180 * RotationInDegrees) * Distance + Position.Y
            };
        }

        public static Block[] GetCollidedBlocks(Vector2 Position, float RotationInDegrees, float Distance)
        {
            return null;
        }
    }
}
