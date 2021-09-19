using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenGL_GameEngine.BeEngine2D;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class Polygon
    {
        Random RadomID = new Random();

        public Polygon()
        {
            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions)
        {
            this.Positions = Positions;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color FillColor)
        {
            this.Positions = Positions;
            this.FillColor = FillColor;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color FillColor, string Tag)
        {
            this.Positions = Positions;
            this.FillColor = FillColor;
            this.Tag = Tag;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color Color, int BorderSize)
        {
            this.Positions = Positions;
            this.Color = Color;
            this.BorderSize = BorderSize;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color Color, int BorderSize, string Tag)
        {
            this.Positions = Positions;
            this.Color = Color;
            this.BorderSize = BorderSize;
            this.Tag = Tag;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color FillColor, Color Color, int BorderSize)
        {
            this.Positions = Positions;
            this.Color = Color;
            this.FillColor = FillColor;
            this.BorderSize = BorderSize;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public Polygon(Vector2[] Positions, Color FillColor, Color Color, int BorderSize, string Tag)
        {
            this.Positions = Positions;
            this.Color = Color;
            this.FillColor = FillColor;
            this.BorderSize = BorderSize;
            this.Tag = Tag;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterPolygon(this);
        }

        public void DestroySelf()
        {
            BeEngine2D.AllPolygons.Remove(this);
            Log.PrintInfo("Removed polygon with ID \"" + ObjectID + "\"");
        }

        public int ObjectID { get; }
        public int BorderSize { get; }
        public Vector2[] Positions { get; set; }
        public Color Color { get; set; }
        public Color FillColor { get; set; }
        public string Tag { get; set; }
    }
}
