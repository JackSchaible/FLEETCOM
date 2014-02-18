using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics
{
    public class Sprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; set; }
        public Sprite(Texture2D texture, Vector2 position, float scale,
            float rotation, float layerDepth)
        {
            Texture = texture;
            Position = position;
            Scale = scale;
            Rotation = rotation;
            LayerDepth = layerDepth;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, 
                Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
        }
    }
}
