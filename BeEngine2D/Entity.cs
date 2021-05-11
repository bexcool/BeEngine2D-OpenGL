using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_GameEngine.BeEngine2D
{
    public class Entity
    {
        Random RadomID = new Random();

        public Entity()
        {
            Position = new Vector2(0, 0);
            Scale = new Vector2(0, 0);

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);
        }

        public Entity(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision, float MoveSpeed)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public Entity(Vector2 Position, Vector2 Scale, Bitmap Bmp, BeEngine2D.CollisionType Collision, float MoveSpeed)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Image = Bmp;
            this.Collision = Collision;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public Entity(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision, float MoveSpeed)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public Entity(Vector2 Position, Vector2 Scale, BeEngine2D.CollisionType Collision, float MoveSpeed, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color.Red;
            this.Collision = Collision;
            this.Tag = Tag;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public Entity(Vector2 Position, Vector2 Scale, Bitmap Bmp, BeEngine2D.CollisionType Collision, float MoveSpeed, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Image = Bmp;
            this.Collision = Collision;
            this.Tag = Tag;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public Entity(Vector2 Position, Vector2 Scale, Color Color, BeEngine2D.CollisionType Collision, float MoveSpeed, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.Collision = Collision;
            this.Tag = Tag;
            this.MoveSpeed = MoveSpeed;

            ObjectID = RadomID.Next() + Convert.ToInt32(DateTime.Now.Millisecond);

            BeEngine2D.RegisterEntity(this);
        }

        public bool IsCollidingByTag(string object_tag)
        {
            if (Collision != BeEngine2D.CollisionType.None)
            {
                foreach (Entity entity in BeEngine2D.AllEntities)
                {
                    if (entity.Tag == object_tag && entity.ObjectID != ObjectID && entity.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > entity.Position.X && Position.X < entity.Position.X + entity.Scale.X &&
                        Position.Y < entity.Position.Y + entity.Scale.Y && Position.Y + Scale.Y > entity.Position.Y) return true;
                }

                foreach (Block block in BeEngine2D.AllBlocks)
                {
                    if (block.Tag == object_tag && block.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
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
                    if (entity.ObjectID != ObjectID && entity.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > entity.Position.X && Position.X < entity.Position.X + entity.Scale.X &&
                        Position.Y < entity.Position.Y + entity.Scale.Y && Position.Y + Scale.Y > entity.Position.Y) return true;
                }

                foreach (Block block in BeEngine2D.AllBlocks)
                {
                    if (block.Collision != BeEngine2D.CollisionType.None && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
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
                    if (block.Collision == BeEngine2D.CollisionType.Overlap && Position.X + Scale.X > block.Position.X && Position.X < block.Position.X + block.Scale.X &&
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
            BeEngine2D.AllEntities.Remove(this);
            Log.PrintInfo("Removed entity with ID \"" + ObjectID + "\"");
        }

        public int ObjectID { get; }
        public BeEngine2D.CollisionType Collision { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Bitmap Image { get; set; }
        public Color Color { get; set; }
        public string Tag { get; set; }
        public float MoveSpeed { get; set; }
    }
}
