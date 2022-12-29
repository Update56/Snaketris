using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        static List<Point> part_pos = new List<Point>();
        static bool flag = false;
        static public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.Three);

            if (gamePadState.IsButtonDown(Buttons.DPadLeft) || keyboardState.IsKeyDown(Keys.Left))
            {
                relocate();
                part_pos[0] -= new Point(1, 0);
            }

            if (gamePadState.IsButtonDown(Buttons.DPadRight) || keyboardState.IsKeyDown(Keys.Right))
            {
                relocate();
                part_pos[0] += new Point(1, 0);
            }

            if (gamePadState.IsButtonDown(Buttons.DPadUp) || keyboardState.IsKeyDown(Keys.Up))
            {
                relocate();
                part_pos[0] -= new Point(0, 1);
            }

            if (gamePadState.IsButtonDown(Buttons.DPadDown) || keyboardState.IsKeyDown(Keys.Down))
            {
                relocate();
                part_pos[0] += new Point(0, 1);
            }
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < part_pos.Count; i++) //отрисовка змеи по частям
            {
                spriteBatch.Draw(part, new Vector2(part_pos[i].X * 32, part_pos[i].Y * 32), Game1.rndcolor);
                // part_pos[i].ToVector2()
            }
        }
        public static void init_list() //инит новой змейки
        {
            part_pos.Clear();
            Random rnd = new Random(Environment.TickCount);
            int temp = rnd.Next(0, 6);
            part_pos.Add(Point.Zero); //1-ый (0-ой) змей элемент от которого идёт создание
            for (int i = 1; i < temp; i++) //создание остальный элементов 
            {
                part_pos.Add(part_pos[i - 1] + new Point(1, 0));
            }
            part_pos.Reverse(); //разворот списка что-бы попа стала головой
        }

        static void relocate() //перемещение тела змейки (см "Visualisation_ver2.gif" )
        {
            for (int i = part_pos.Count - 1; i > 0; i--)
            {
                part_pos[i] = part_pos[i - 1];
            }
        }
    }
}
