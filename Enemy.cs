using SFML.Graphics;
using SFML.System;

public class Enemy
{
    public CircleShape Shape = new CircleShape()
    {
        Radius = 0.025f * Math.Min(Window.window.Size.X, Window.window.Size.Y),

        Position = new Vector2f(0, 0),

        FillColor = Color.Black,
        OutlineColor = Color.White,
        OutlineThickness = 1
    };

    public int MaxHP = 100;
    public int CurrentHP = 100;
    public int Damage = 1;

    public Vector2f DirectionSign = new Vector2f();
    public float CenterCoefficient = 1;
    public float TangentCoefficient = 1;

    public Vector2f Center()
    {
        return new Vector2f(Shape.Position.X + Shape.Radius, Shape.Position.Y + Shape.Radius);
    }

    public void Move()
    {
        Vector2f player = new Vector2f(Player.Shape.Radius, Player.Shape.Radius);
        Vector2f enemy = new Vector2f(Shape.Radius, Shape.Radius);

        Vector2f center = Vector.Normalize(Player.Shape.Position - Shape.Position + player - enemy);
        Vector2f tangent = new Vector2f(DirectionSign.X * center.Y, DirectionSign.Y * center.X);

        Shape.Position += CenterCoefficient * center + TangentCoefficient * tangent;
    }
}

public static class Enemies
{
    public static List<Enemy> enemies = new List<Enemy>();

    private static bool Collision(Enemy enemy)
    {
        return Player.Shape.Radius + enemy.Shape.Radius > Vector.Length(Player.Center(), enemy.Center());
    }

    public static bool Hit(int Index, int Damage)
    {
        enemies[Index].CurrentHP -= Damage;

        if (enemies[Index].CurrentHP <= 0)
        {
            enemies.RemoveAt(Index);

            return true;
        }

        return false;
    }

    public static void Move()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Collision(enemies[i]))
            {
                Player.Hit(enemies[i].Damage);

                enemies.RemoveAt(i);
            }
            else
            {
                enemies[i].Move();
            }
        }
    }

    public static void Draw()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Window.window.Draw(enemies[i].Shape);
        }
    }

    public static void New()
    {
        if (enemies.Count >= 0)
        {
            Random random = new Random();
            Enemy enemy = new Enemy();

            // set random direction
            if (random.Next() % 2 == 0)
            {
                enemy.DirectionSign = new Vector2f(-1, 1);
            }
            else
            {
                enemy.DirectionSign = new Vector2f(1, -1);
            }

            //set random moving coeffc
            enemy.CenterCoefficient = random.Next(1, 2);
            enemy.TangentCoefficient = random.Next(1, 20);

            //set random x and y position
            Vector2f Position = new Vector2f();

            if (random.Next() % 2 == 0)
            {
                Position.X = 0;
            }
            else
            {
                Position.X = Window.window.Size.X - 2 * enemy.Shape.Radius;
            }

            if (random.Next() % 2 == 0)
            {
                Position.Y = 0;
            }
            else
            {
                Position.Y = Window.window.Size.Y - 2 * enemy.Shape.Radius;
            }

            enemy.Shape.Position = Position;

            enemies.Add(enemy);
        }
    }
}