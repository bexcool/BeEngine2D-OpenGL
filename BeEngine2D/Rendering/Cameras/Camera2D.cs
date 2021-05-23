using OpenGL_GameEngine.BeEngine2D.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D.Rendering.Cameras
{
    class Camera2D
    {
        public Vector2 FocusPosition { get; set; }
        public float Zoom { get; set; }
        public string FocusedObjectTag { get; set; }

        public Camera2D (Vector2 FocusPosition, float Zoom)
        {
            this.FocusPosition = FocusPosition;
            this.Zoom = Zoom;
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            float Left = FocusPosition.X - DisplayManager.WindowSize.X / 2f;
            float Right = FocusPosition.X + DisplayManager.WindowSize.X / 2f;
            float Top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f;
            float Bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;

            Matrix4x4 OrthoMatrix = Matrix4x4.CreateOrthographicOffCenter(Left, Right, Bottom, Top, 0.01f, 100f);
            Matrix4x4 ZoomMatrix = Matrix4x4.CreateScale(Zoom);

            return OrthoMatrix * ZoomMatrix;
        }

        public void FollowObjectByTag(string Tag)
        {
            FocusedObjectTag = Tag;
        }
    }
}
