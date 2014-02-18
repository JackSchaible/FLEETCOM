using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FleetCom.Graphics;
using FleetCom.Graphics.UI;


namespace FleetCom
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GalaxyMap : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Sprite background;
        Sprite Tutorial1;

        Button OKButton;

        public GalaxyMap(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);

            background = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>(@"Graphics/Environments/starfield"),
                Vector2.Zero, 1.0f, 0.0f, 0.0f);

            Tutorial1 = new Sprite(
                ((Game1)Game).Content.Load<Texture2D>("Graphics/IncursionMap/Tutorial1"),
                new Vector2(450, 325), 1.0f, 0.0f, 1.0f);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            foreach (StarCluster item in ((Game1)Game).User.Map.StarClusters)
                item.Update(state);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            background.Draw(spriteBatch);

            foreach (StarCluster item in ((Game1)Game).User.Map.StarClusters)
                item.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
