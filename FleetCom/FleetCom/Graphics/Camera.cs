using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics
{
    public class Camera
    {
        protected Viewport _viewport;
        protected MouseState _mState;
        protected KeyboardState _keyState;

        public Matrix Translation { get; set; }
        public Matrix InverseTransform { get; set; }
        public float Zoom { get; set; }

        public Vector2 Position;
        public delegate void PositionChange();
        public PositionChange PositionChanged;

        public Camera(Viewport viewport)
        {
            Position = Vector2.Zero;
            _viewport = viewport;
            Zoom = 1.0f;
        }

        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.A))
            {
                Position.X += 20f;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                Position.X -= 20f;
            }
            if (keyState.IsKeyDown(Keys.W))
            {
                Position.Y -= 20f;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                Position.Y += 20f;
            }
            if (keyState.IsKeyDown(Keys.C))
                Zoom += 0.01f;
            if (keyState.IsKeyDown(Keys.Z))
                Zoom -= 0.01f;

            Translation = Matrix.CreateTranslation(Position.X, Position.Y, 0);
            InverseTransform = Matrix.Invert(Translation);
        }
    }
}
