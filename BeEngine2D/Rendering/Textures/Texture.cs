using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using static OpenGL_GameEngine.BeEngine2D.GL;
using Bitmap = System.Drawing.Bitmap;
using Image = System.Drawing.Image;
using StbiSharp;
using System.IO;

namespace OpenGL_GameEngine.BeEngine2D.Rendering.Textures
{
    public class Texture
    {
        private string FilePath;
        private uint TexID;

        public unsafe Texture (string FilePath)
        {
            this.FilePath = FilePath;

            TexID = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, TexID);

            // Set texture parameters
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

            // When stretching the image, pixelate
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);

            // When shrinking the image, pixelate
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

            if (FilePath != null)
            {
                Bitmap Image = (Bitmap)Bitmap.FromFile(FilePath);

                Image.RotateFlip(RotateFlipType.RotateNoneFlipY);

                if (Image != null)
                {
                    BitmapData BmpData = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, BmpData.Width, BmpData.Height, 0, GL_RGBA, GL_UNSIGNED_BYTE, BmpData.Scan0);

                    Image.UnlockBits(BmpData);
                }
                else
                {
                    Log.PrintError("Can't load image \"" + FilePath + "\"");
                }

                Image.Dispose();
            }
        }

        public void Bind()
        {
            glBindTexture(GL_TEXTURE_2D, TexID);
        }

        public void UnBind()
        {
            glBindTexture(GL_TEXTURE_2D, 0);
        }
    }
}
