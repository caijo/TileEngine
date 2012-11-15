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

namespace TileEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap myMap = new TileMap();
        int squaresAcross = 18;
        int squaresDown = 11;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\part2_tileset");


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0,
                    (myMap.MapWidth - squaresAcross) * Tile.TileWidth);
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0,
                    (myMap.MapWidth - squaresAcross) * Tile.TileWidth);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0,
                    (myMap.MapHeight - squaresDown) * Tile.TileHeight);
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0,
                    (myMap.MapHeight - squaresDown) * Tile.TileHeight);
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileWidth, Camera.Location.Y / Tile.TileHeight);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileWidth, Camera.Location.Y % Tile.TileHeight);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown; y++)
            {
                for (int x = 0; x < squaresAcross; x++)
                {
                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileWidth) - offsetX, (y * Tile.TileHeight) - offsetY,
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
