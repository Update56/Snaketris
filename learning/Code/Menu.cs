using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace learning
{
    internal static class Menu
    {
        public static Texture2D Background { get; set; }
        public static Texture2D MenuSprite { get; set; }
        static int timeCounter = 0;
        static Color color;
        public static SpriteFont Font { get; set; }
        static float MenuSpriteRotate;
        static float MenuSpriteScale;

        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, 640, 960), Color.White);
            //spriteBatch.DrawString(Font, "Snaketris", new Vector2(190, 250), Game1.GetRandomColor());
            spriteBatch.Draw(MenuSprite, new Vector2(300, 250), null, Color.White, MenuSpriteRotate, new Vector2(MenuSprite.Width / 2f, MenuSprite.Height / 2f), MenuSpriteScale, SpriteEffects.None, 0f);
        }
        static public void Update()
        {
            MenuSpriteScale = (float)(Math.Sin(timeCounter * (Math.PI / 180.0) / 4) / 4 + 1);
            MenuSpriteRotate = (float)(Math.Sin(timeCounter * (Math.PI / 180.0))/4);
            //color = Color.FromNonPremultiplied(255, 255, 255, Convert.ToInt32(Math.Abs(Math.Sin(timeCounter * (Math.PI / 180.0) / 8) * 256.0)));
            timeCounter++;
        }
    }
}
