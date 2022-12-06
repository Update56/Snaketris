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

namespace learning
{

    internal class Snake
    {
        public static Texture2D part { get; set; }
        static List<Vector2> part_pos = new List<Vector2>();
        static public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                relocate();
                part_pos[0] = new Vector2(part_pos[0].X - Game1.speed, part_pos[0].Y);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                relocate();
                part_pos[0] = new Vector2(part_pos[0].X + Game1.speed, part_pos[0].Y);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                relocate();
                part_pos[0] = new Vector2(part_pos[0].X, part_pos[0].Y - Game1.speed);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                relocate();
                part_pos[0] = new Vector2(part_pos[0].X, part_pos[0].Y + Game1.speed);
            }
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < part_pos.Count; i++) //отрисовка змеи по частям
            {
                spriteBatch.Draw(part, part_pos[i], Game1.rndcolor);
            }
            
        }
        public static void init_list() //инит новой змейки
        {
            part_pos.Clear();
            Random rnd = new Random();
            int temp = rnd.Next(0, 6);
            part_pos.Add(Vector2.Zero); //1-ый (0-ой) змей элемент от которого идёт создание
            for (int i = 1; i < temp; i++) //создание остальный элементов 
            {
                part_pos.Add(new Vector2(part_pos[i - 1].X + Game1.speed, part_pos[i - 1].Y));
            }

            //part_pos[0] = Vector2.Zero; //1-ый (0-ой) змей элемент от которого идёт создание
            //for (int i = 1; i < part_pos.Count; i++) //установка координаты для каждой новой части
            //{
            //    part_pos[i] = new Vector2(part_pos[i - 1].X + Game1.speed, part_pos[i - 1].Y);
            //}

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
