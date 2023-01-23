using System;
using System.Timers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using learning.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using SharpDX.XInput;

namespace learning
{

    internal class Snake
    {
        public static Texture2D part { get; set; }
        static public List<Point> snake_pos = new List<Point>();
        static bool control = true; 
        static Direction direction;
        static KeyboardState keyboardState, oldkeyboardState;
        static GamePadState gamePadState;
        enum Direction //направления движения
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
        static public void Update()
        {
            switch (direction)
            {
                case Direction.LEFT:
                    Relocate();
                    snake_pos[0] -= new Point(1, 0);
                    break;
                case Direction.RIGHT:
                    Relocate();
                    snake_pos[0] += new Point(1, 0);
                    break;
                case Direction.UP:
                    Relocate();
                    snake_pos[0] -= new Point(0, 1);
                    break;
                case Direction.DOWN:
                    Relocate();
                    snake_pos[0] += new Point(0, 1);
                    break;
            }
        }
        static public void Control()
        {
            oldkeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);

            switch (direction)
            {
                case Direction.LEFT:
                case Direction.RIGHT:
                    if (gamePadState.IsButtonDown(Buttons.DPadDown) || keyboardState.IsKeyDown(Keys.Down))
                        direction = Direction.DOWN;
                    else if (gamePadState.IsButtonDown(Buttons.DPadUp) || keyboardState.IsKeyDown(Keys.Up))
                        direction = Direction.UP;
                    break;
                case Direction.UP:
                case Direction.DOWN:
                    if (gamePadState.IsButtonDown(Buttons.DPadLeft) || keyboardState.IsKeyDown(Keys.Left))
                        direction = Direction.LEFT;
                    else if (gamePadState.IsButtonDown(Buttons.DPadRight) || keyboardState.IsKeyDown(Keys.Right))
                        direction = Direction.RIGHT;
                    break;
            }

            if (gamePadState.IsButtonDown(Buttons.A) || keyboardState.IsKeyDown(Keys.Enter) && !oldkeyboardState.IsKeyDown(Keys.Enter))
            {
                Game1.Freeze_time();
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < snake_pos.Count; i++) //отрисовка змеи по частям
                spriteBatch.Draw(part, new Vector2(snake_pos[i].X * 32, snake_pos[i].Y * 32), Game1.rndcolor);
        }
        public static void Init_snake() //инит новой змейки
        {
            snake_pos.Clear();
            Random rnd = new Random(Environment.TickCount);
            int temp = rnd.Next(0, 6);
            snake_pos.Add(Point.Zero); //1-ый (0-ой) змей элемент от которого идёт создание

            for (int i = 1; i < temp; i++) //создание остальных элементов 
                snake_pos.Add(snake_pos[i - 1] + new Point(1, 0));

            snake_pos.Reverse(); //разворот списка что-бы попа стала головой

            direction = Direction.RIGHT;
        }

        public static void Stop_snake()
        {
            control = false;
            Field.Freezing(snake_pos);
            snake_pos.Clear();
            Init_snake();
        }
        static void Relocate() //перемещение тела змейки (см "Visualisation_ver2.gif" )
        {
            for (int i = snake_pos.Count - 1; i > 0; i--)
                snake_pos[i] = snake_pos[i - 1];
        }
        static public void Control_on() => control = true;
    }
}
