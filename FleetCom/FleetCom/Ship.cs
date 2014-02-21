using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom
{
    public class Ship : Graphics.ISprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public List<ResearchItem> Prerequisites { get; set; }
        public Ship(List<ResearchItem> prerequisites)
        {
            Prerequisites = prerequisites;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
