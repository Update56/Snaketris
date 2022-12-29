using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Web;

namespace learning
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D texture, apple;
        Vector2 pos = Vector2.Zero;
        public const int speed = 32;
        int period = 100;
        int currentTimeUpd = 0;
        public static Color rndcolor = GetRandomColor();
        public int windowheight = 960;
        public int windowwidth = 640;
        
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = windowheight;
            _graphics.PreferredBackBufferWidth = windowwidth;
            _graphics.ApplyChanges();
            Snake.init_list();
            Field.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Part_32x32");
            Snake.part = Content.Load<Texture2D>("Part_32x32");
            apple = Content.Load<Texture2D>("apple");
            Menu.Background = Content.Load<Texture2D>("background");
            Menu.Font = Content.Load<SpriteFont>("MC");
            Menu.MenuSprite = Content.Load<Texture2D>("MenuSprite");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentTimeUpd += gameTime.ElapsedGameTime.Milliseconds; //Ограничение fps

            if (currentTimeUpd > period)
            {
                currentTimeUpd -= period;

                Snake.Update();
            };
            Menu.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSteelBlue);

            _spriteBatch.Begin();
            Menu.Draw(_spriteBatch);
            Snake.Draw(_spriteBatch);
            Field.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 255);
            int g = rnd.Next(0, 255);
            int b = rnd.Next(0, 255);
            Color RandColor = new(r, g, b);
            return RandColor;
        }
    }
}