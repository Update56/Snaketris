using learning.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MessageBox = System.Windows.Forms.MessageBox;

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
        public int windowwidth = 790; //640
        public static int score = 0;
        string status_bar;

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
            Snake.Init_snake();
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
            }

            if (Snake.snake_pos[0].X == Field.field_size_x || Snake.snake_pos[0].X == -1 || Snake.snake_pos[0].Y == -1 || Snake.snake_pos[0].Y == 10)
            {
                MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Exit();
            }

            foreach (var item in Snake.snake_pos.GetRange(1, Snake.snake_pos.Count - 1))
            {
                if (Snake.snake_pos[0] == item)
                {
                    MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Exit();
                }
            }

            status_bar = "Time: \n" + (gameTime.TotalGameTime.Minutes / 10 % 10).ToString() + (gameTime.TotalGameTime.Minutes % 10).ToString() + ':'
                + (gameTime.TotalGameTime.Seconds / 10 % 10).ToString() + (gameTime.TotalGameTime.Seconds % 10).ToString()
                + "\nScore:\n" + score.ToString();
            Field.Update();
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
            _spriteBatch.DrawString(Menu.Font, status_bar, new Vector2(650, 10), Color.DarkSlateBlue);


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