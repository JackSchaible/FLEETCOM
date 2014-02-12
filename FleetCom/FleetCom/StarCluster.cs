using FleetCom.Graphics;
using FleetCom.Graphics.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom
{
    [Serializable]
    public enum StarClusterStates
    {
        Unowned,
        UnderAttack,
        Owned
    }
    public delegate void OnSelected();

    [Serializable]
    public class StarCluster
    {
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public StarClusterStates State { get; set; }
        public OnSelected ClusterSelected;

        private Texture2D Texture
        {
            get
            {
                Texture2D result = NormalTexture;

                switch(State)
                {
                    case StarClusterStates.Owned:
                        result = OwnedTexture;
                        break;

                    case StarClusterStates.UnderAttack:
                        result = UnderAttackTexture;
                        break;

                    case StarClusterStates.Unowned:
                        result = NormalTexture;
                        break;
                }

                return result;
            }
        }
        private Texture2D NormalTexture;
        private Texture2D UnderAttackTexture;
        private Texture2D OwnedTexture;
        private Rectangle rectangle;

        public StarCluster(Vector2 position, string name, Texture2D normalTexture,
            Texture2D underAttackTexture, Texture2D ownedTexture, StarClusterStates state)
        {
            Position = position;
            Name = name;
            State = state;

            NormalTexture = normalTexture;
            UnderAttackTexture = underAttackTexture;
            OwnedTexture = ownedTexture;

            rectangle = new Rectangle((int)Position.X, (int)Position.Y,
                    Texture.Width,
                    Texture.Height);
        }

        public void Update(MouseState state)
        {
            if (rectangle.Contains(new Point(state.X, state.Y)))
                if (state.LeftButton == ButtonState.Pressed)
                    ClusterSelected();
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0.0f,
                Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
