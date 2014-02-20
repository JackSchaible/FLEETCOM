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
    public enum StarClusterStates
    {
        Unowned,
        UnderAttack,
        Owned
    }
    public delegate void OnSelected(StarCluster sender);

    public class StarCluster
    {
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public StarClusterStates State { get; set; }
        public OnSelected ClusterSelected;
        public List<StarSystem> StarSystems;
        public ButtonStates ButtonState;

        private AnimatedSprite Texture
        {
            get
            {
                AnimatedSprite result = NormalTexture;

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
        private AnimatedSprite NormalTexture;
        private AnimatedSprite UnderAttackTexture;
        private AnimatedSprite OwnedTexture;
        private ClusterStatus CurrentSystemStatus;
        private Rectangle rectangle;

        public StarCluster(Vector2 position, string name, Texture2D normalTexture,
            Texture2D underAttackTexture, Texture2D ownedTexture, Texture2D statusWindowTexture,
            SpriteFont MH45, SpriteFont MH75, StarClusterStates state)
        {
            Position = position;
            Name = name;
            State = state;

            NormalTexture = new AnimatedSprite(200, 200, normalTexture, Position, MH45);
            UnderAttackTexture = new AnimatedSprite(200, 200, underAttackTexture, Position, MH45);
            OwnedTexture = new AnimatedSprite(200, 200, ownedTexture, Position, MH45);

            rectangle = new Rectangle((int)Position.X, (int)Position.Y, 200, 200);

            //Todo: Instantiate star systems
            StarSystems = new List<StarSystem>();

            CurrentSystemStatus = new ClusterStatus(this, statusWindowTexture, GetStatusWindowPosition(), MH45, MH75);
        }

        public void Update(MouseState state)
        {
            if (rectangle.Contains(new Point(state.X, state.Y)))
            {
                ButtonState = ButtonStates.Hover;

                if (state.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    ClusterSelected(this);
                    ButtonState = ButtonStates.Pressed;
                }
            }
            else
                ButtonState = ButtonStates.Normal;

            CurrentSystemStatus.Update();
            Texture.Update();
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            Texture.Draw(spriteBatch);

            if (ButtonState == ButtonStates.Hover)
                CurrentSystemStatus.Draw(spriteBatch);
        }

        private Vector2 GetStatusWindowPosition()
        {
            Vector2 result = new Vector2(Position.X + Texture.FrameWidth + 25,
                Position.Y + (Texture.FrameHeight / 2) + 25);

            if (Position.X >= 1400)
            {
                if (Position.Y >= 1000)
                    result = new Vector2(Position.X - 425, Position.Y - 200);
                else
                    result = new Vector2(Position.X - 425, Position.Y);
            }

            return result;
        }
    }
}
