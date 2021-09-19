using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class Block
    {
        Random RadomID = new Random();

        public Block()
        {
            Position = new Vector2(0, 0);
            Scale = new Vector2(0, 0);
            Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);
        }

        public Block(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, string ImageURL, BeEngine2D.CollisionType Collision)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.ImageURL = ImageURL;
            this.Collision = Collision;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, string ImageURL, BeEngine2D.CollisionType Collision, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.ImageURL = ImageURL;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = 0;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, string ImageURL, BeEngine2D.CollisionType Collision, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.ImageURL = ImageURL;
            this.Collision = Collision;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision, string Tag, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, string ImageURL, BeEngine2D.CollisionType Collision, string Tag, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.ImageURL = ImageURL;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public Block(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision, string Tag, int Z_Index)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.Tag = Tag;
            this.Z_Index = Z_Index;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterBlock(this);
        }

        public bool IsCollidingByTag(string object_tag)
        {
            if (Collision != BeEngine2D.CollisionType.None)
            {
                foreach (Entity entity in BeEngine2D.AllEntities)
                {
                    if (entity.Tag == object_tag && entity.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > entity.Position.X && Position.X < entity.Position.X + entity.Scale.X &&
                        Position.Y < entity.Position.Y + entity.Scale.Y && Position.Y + Scale.Y > entity.Position.Y) return true;
                }

                foreach (Block block in BeEngine2D.AllBlocks)
                {
                    if (block.Tag == object_tag && block.ObjectID != ObjectID && block.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
                        Position.Y < block.Position.Y + block.Scale.Y && Position.Y + Scale.Y > block.Position.Y) return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public bool IsColliding()
        {
            if (Collision != BeEngine2D.CollisionType.None)
            {
                foreach (Entity entity in BeEngine2D.AllEntities)
                {
                    if (entity.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > entity.Position.X && Position.X < entity.Position.X + entity.Scale.X &&
                        Position.Y < entity.Position.Y + entity.Scale.Y && Position.Y + Scale.Y > entity.Position.Y) return true;
                }

                foreach (Block block in BeEngine2D.AllBlocks)
                {
                    if (block.Collision != BeEngine2D.CollisionType.None && block.ObjectID != ObjectID && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
                        Position.Y < block.Position.Y + block.Scale.Y && Position.Y + Scale.Y > block.Position.Y) return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public Block[] GetCollidingBlocks()
        {
            List<Block> blocks = new List<Block>();

            if (Collision != BeEngine2D.CollisionType.None)
            {
                foreach (Block block in BeEngine2D.AllBlocks)
                {
                    if (block.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
                        Position.Y < block.Position.Y + block.Scale.Y && Position.Y + Scale.Y > block.Position.Y) blocks.Add(block);
                }

                return blocks.ToArray();
            }
            else
            {
                return null;
            }
        }

        public Entity[] GetCollidingEntities()
        {
            List<Entity> entities = new List<Entity>();

            if (Collision != BeEngine2D.CollisionType.None)
            {
                foreach (Entity entity in BeEngine2D.AllEntities)
                {
                    if (entity.ObjectID != ObjectID && entity.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > entity.Position.X && Position.X < entity.Position.X + entity.Scale.X &&
                        Position.Y < entity.Position.Y + entity.Scale.Y && Position.Y + Scale.Y > entity.Position.Y) entities.Add(entity);
                }

                return entities.ToArray();
            }
            else
            {
                return null;
            }
        }

        public void DestroySelf()
        {
            BeEngine2D.AllBlocks.Remove(this);
            Log.PrintInfo("Removed block with ID \"" + ObjectID + "\"");
        }

        public int ObjectID { get; }

        public int Z_Index { get; set; }
        public BeEngine2D.CollisionType Collision { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public string ImageURL { get; set; }
        public Color Color { get; set; }
        public string Tag { get; set; }
    }
}
