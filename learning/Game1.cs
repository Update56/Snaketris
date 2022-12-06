﻿using Microsoft.Xna.Framework;
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
        int speed = 32;
        int period = 100;
        int currentTimeUpd = 0;
        Color rndcolor = GetRandomColor();
        public int windowheight = 960;
        public int windowwidth = 640;
        List<Vector2> snake = new List<Vector2>();

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
            snake.Add(Vector2.Zero);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Part_32x32");
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

            if (windowwidth - pos.X - speed <= 0 )
            {
                pos.X = windowwidth - speed;
            }
            if (windowheight - pos.Y - speed <= 0)
            {
                pos.Y = windowheight - speed;
            }
            if (pos.X <= 0)
            {
                pos.X = 0;
            }
            if (pos.Y <= 0)
            {
               pos.Y = 0;
            }

                if (currentTimeUpd > period)
                {
                currentTimeUpd -= period;
                
                    if (keyboardState.IsKeyDown(Keys.Left))
                    {
                        snake[0] = new Vector2 { X = snake[0].X - speed, Y = snake[0].Y};
                        pos.X -= speed;
                        rndcolor = GetRandomColor();
                    }

                    if (keyboardState.IsKeyDown(Keys.Right))
                    {
                        pos.X += speed;
                        rndcolor = GetRandomColor();
                    }

                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        pos.Y -= speed;
                        rndcolor = GetRandomColor();
                    }

                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        pos.Y += speed;
                        rndcolor = GetRandomColor();
                    }
                

            };
            Menu.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSteelBlue);

            _spriteBatch.Begin();
            Menu.Draw(_spriteBatch);
            _spriteBatch.Draw(texture, pos, rndcolor);
            _spriteBatch.Draw(texture, new Vector2(pos.X, pos.Y + speed), rndcolor);
            _spriteBatch.Draw(apple, new Vector2(10 * speed, 25 * speed), Color.White);
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