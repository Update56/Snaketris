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
        public const int speed = 32;
        int period = 200;
        int currentTimeUpd = 0;
        public int windowheight = 960;
        public int windowwidth = 780; //640
        public static int score = 0;
        string status_bar;
        public static int time_check = 5;
        static int seconds = 0;
        

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Snake.part = Content.Load<Texture2D>("Part_32x32");
            Menu.Background = Content.Load<Texture2D>("background");
            Menu.Font = Content.Load<SpriteFont>("MC");
            Menu.MenuSprite = Content.Load<Texture2D>("MenuSprite");
            Field.Vines = Content.Load<Texture2D>("InvSprite");
        }

        protected override void Update(GameTime gameTime)
        {
            seconds = gameTime.TotalGameTime.Seconds;
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

             if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
                MessageBox.Show("Данная игра является продуктом, полученным в результате совмещения двух игр: Snake и Tetris и работает по совмещённым правилам.\r\nИгра начинается с того, что из левого верхнего угла по горизонтали выползает змейка случайной длины, состоящая из 1-5 блоков, которой пользователь может управлять в пределах верхней части игрового поля, которая отделена от нижней части лианами. \r\nЗадача игрока заключается в том, чтобы за определённое время, которое зависит от длины змейки (для одного блока - 5 секунд, для двух - 6 секунд, и т.д. +1 секунда за каждый блок), сформировать змейкой определённую фигуру, которая по нажатию клавиши \"Enter\" на клавиатуре или кнопки \"А\" на геймпаде или по истечению времени  \"упадёт\" в нижнюю часть игрового поля, предназначеную для тетриса (вся нижняя часть поля до лиан). При заполнении фигурами полного ряда составленный ряд исчезает и игроку начисляются 10 очков. В случае, если фигуры на поле тетриса  \"пересекают\" лианы или если змейка выходит за пределы своего игрового поля или врезается в хвот, игра считается оконченной.", "Правила", MessageBoxButtons.OK, MessageBoxIcon.Information);

            currentTimeUpd += gameTime.ElapsedGameTime.Milliseconds; //Ограничение fps

            if (currentTimeUpd > period)
            {
                currentTimeUpd -= period; 
                Snake.Update();
            }

            Snake.Control();

            if (Snake.snake_pos[0].X == Field.field_size_x || Snake.snake_pos[0].X == -1 || Snake.snake_pos[0].Y == -1 || Snake.snake_pos[0].Y == 10)
            {
                Game_over();
            }

            for (int x = 0; x < Field.field_size_x - 1; x++)
            {
                if (Field.game_field[x, 9])
                {
                    Game_over();
                    break;
                }
            }

            foreach (var item in Snake.snake_pos.GetRange(1, Snake.snake_pos.Count - 1))
            {
                if (Snake.snake_pos[0] == item)
                {
                    Game_over();
                }
            }

            if (time_check == gameTime.TotalGameTime.Seconds)
            {
                Freeze_time();
            }

            status_bar = "Freezing\nin: " + ( time_check - gameTime.TotalGameTime.Seconds).ToString() + "\nScore:\n" + score.ToString();
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
            _spriteBatch.DrawString(Menu.Font, "\nTime: \n" + (gameTime.TotalGameTime.Minutes / 10 % 10).ToString() + (gameTime.TotalGameTime.Minutes % 10).ToString() + ':'
                + (gameTime.TotalGameTime.Seconds / 10 % 10).ToString() + (gameTime.TotalGameTime.Seconds % 10).ToString() + "\nf1 : help", new Vector2(650, windowheight - 135), Color.DarkSlateBlue) ;

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
        static public void Freeze_time()
        {
            Snake.Stop_snake();
            time_check = seconds + Snake.snake_pos.Count + 4;
        }
        public void Game_over()
        {
            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Exit();
        }
    }
}