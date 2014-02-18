using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics.UI
{
    public class ClusterStatus : Sprite
    {
        public StarCluster Cluster { get; set; }
        public SpriteFont Font { get; set; }
        public SpriteFont NumberFont { get; set; }

        string Name;
        string NumberOfSystems;
        string NumberOfBases;

        Vector2 NamePosition, NOSPosition, NOBPosition;

        public ClusterStatus(StarCluster cluster, Texture2D Texture, Vector2 Position, SpriteFont font,
            SpriteFont numberFont)
            :base(Texture, Position, 1.0f, 0.0f, 1.0f)
        {
            Cluster = cluster;
            Font = font;
            NumberFont = numberFont;

            Name = cluster.Name;
            NumberOfSystems = cluster.StarSystems.Count.ToString();
            NumberOfBases = cluster.StarSystems.Where(x => x.HasBase).Count().ToString();

            NamePosition = new Vector2(Position.X + 20, Position.Y + 15);
            NOSPosition = new Vector2(Position.X + 145, Position.Y + 75);
            NOBPosition = new Vector2(Position.X + 330, Position.Y + 75);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.9f);
            spriteBatch.DrawString(Font, Name, NamePosition, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(NumberFont, NumberOfSystems, NOSPosition, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(NumberFont, NumberOfBases, NOBPosition, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
