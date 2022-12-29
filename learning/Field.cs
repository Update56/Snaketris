using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace learning
{
    internal static class Field
    {
        public const int field_size_x = 20;
        public const int field_size_y = 30;
        static bool[,] game_field = new bool[field_size_x, field_size_y];
        static public void Initialize()
        {
            
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < field_size_y; i++)
                for (int j = 0; j < field_size_x; j++)
                    if (game_field[j,i] == true)
                        spriteBatch.Draw(Snake.part, new Vector2(i * 32, j * 32), Game1.rndcolor);
        }

    }
}
