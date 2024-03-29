﻿using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks.Sources;

namespace learning.Code
{
    internal static class Field
    {
        public const int field_size_x = 20; 
        public const int field_size_y = 30;
        static public bool[,] game_field = new bool[field_size_x, field_size_y];
        static public Texture2D Vines;
        static public void Update()
        {
            for (int y = 0; y < field_size_y; y++)
            {
                Line_fullness(y);
            }
            
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < field_size_y; y++)
                for (int x = 0; x < field_size_x; x++)
                    if (game_field[x, y] == true)
                        spriteBatch.Draw(Snake.part, new Vector2(x * Game1.speed, y * Game1.speed), Color.Silver);
            spriteBatch.Draw(Vines, new Vector2(0, 304), Color.White);
        }
        static public void Freezing(List<Point> snake) 
        {
            bool flag = true;
            do
            {
                foreach (Point el in snake) //проверка на наличие блока или пола 
                {
                    if (el.Y == field_size_y - 1)
                    {
                        flag = false;
                        break;
                    }
                    else if (game_field[el.X, el.Y + 1] == true)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) //если всё ок то опускаем змейку
                {
                    for (int i = 0; i < snake.Count; i++)
                    {
                        snake[i] += new Point(0, 1);
                    }
                }
            } while (flag);

            foreach (Point el in snake) //заполнение поля
            {
                game_field[el.X, el.Y] = true;
            }
            Snake.Control_on();
        }
        static void Line_fullness(int y) //заполненности одной линии
        {
            for (int x = 0; x < field_size_x; x++) // проверка 
            {
                if (!game_field[x, y])
                {
                    return;
                }
            }
            for (int u = y; u >= 1; u--) //смещение если заполнена
            {
                for (int x = 0; x < field_size_x; x++)
                {
                    game_field[x, u] = game_field[x, u - 1];
                }
            }
            Game1.score += 10;

            return;
        }
    }
}
