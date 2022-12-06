using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace learning
{
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    internal class Snake
    {
        private List<Point> snake;
        private Direction direction;
        private int step = 1;
        private Point tail;
        private Point head;
        bool rotate = true;

        public Snake(int x, int y, int length)
        {
            direction = Direction.RIGHT;
            snake = new List<Point>();
            for (int i = x - length; i < x; i++)
            {
                Point p = new(i, y);
                snake.Add(p);
            }
        }
        public Point GetHead() => snake.Last();
        public Point GetNextPoint()
        {
            Point p = GetHead();

            switch (direction)
            { 
                case Direction.LEFT:
                    p.X -= step;
                    break;
                case Direction.RIGHT:
                    p.X += step;
                    break;
                case Direction.UP:
                    p.Y -= step;
                    break;
                case Direction.DOWN:
                    p.Y += step;
                    break;
            }
            return p;
        }
        public void Move()
        {
            head = GetNextPoint();
            snake.Add(head);

            tail = snake.First();
            snake.Remove(tail);

            rotate = true;
        }
    }
}
