using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FleetCom.Graphics
{
    public class ResearchCamera
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

        public ResearchCamera(Viewport viewport)
        {
            Position = Vector2.Zero;
            _viewport = viewport;
            Zoom = 1.0f;
        }

        public void Update(MouseState state)
        {
            if (state.X < 5)
                if (Position.X < 0)
                    Position.X += 20f;

            if (state.Y < 5)
                if (Position.Y <= 1250)
                    Position.Y += 20f;

            if (state.X > 1915)
                if (Position.X > -1900)
                    Position.X -= 20f;

            if (state.Y > 1075)
                if (Position.Y >= -1250)
                    Position.Y -= 20f;

            Translation = Matrix.CreateTranslation(Position.X, Position.Y, 0);
            InverseTransform = Matrix.Invert(Translation);
        }
    }
}
