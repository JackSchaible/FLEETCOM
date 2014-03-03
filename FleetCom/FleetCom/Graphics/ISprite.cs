using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics
{
    public interface ISprite
    {
        Texture2D Texture { get; }
        Vector2 Position { get; set; }

        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
