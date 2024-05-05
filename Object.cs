using SFML.Window;
using SFML.Graphics;
using SFML.System;

public static class Player
{
    public static int Health = 10;

    public static CircleShape Shape = new CircleShape()
    {
        Radius = 0.05f * Math.Min(Window.window.Size.X, Window.window.Size.Y),

        Position = Window.Center(0.05f * Math.Min(Window.window.Size.X, Window.window.Size.Y)),

        FillColor = Color.White,
        OutlineColor = Color.Black,
        OutlineThickness = 1
    };

    public static void Draw()
    {
        Window.window.Draw(Shape);
    }

    public static Vector2f Center()
    {
        return new Vector2f(Shape.Position.X + Shape.Radius, Shape.Position.Y + Shape.Radius);
    }

    public static void Hit(int Damage)
    {
        Health -= Damage;

        if (Health <= 0)
        {
            Window.window.Close();
        }
    }
}

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

    public int Health = 1;
    public int Damage = 1;

    public Vector2f Center()
    {
        return new Vector2f(Shape.Position.X + Shape.Radius, Shape.Position.Y + Shape.Radius);
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
        enemies[Index].Health -= Damage;

        if (enemies[Index].Health <= 0)
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
                Vector2f u = Vector.Normalize(Player.Shape.Position - enemies[i].Shape.Position);
                Vector2f v = new Vector2f(u.Y, -u.X);

                enemies[i].Shape.Position += 5 * u + 3 * v;
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
        if (enemies.Count < 5)
        {
            enemies.Add(new Enemy());
        }
    }
}

public class Bullet
{
    public CircleShape Shape = new CircleShape()
    {
        Radius = 0.01f * Math.Min(Window.window.Size.X, Window.window.Size.Y),

        Position = Window.Center(0.01f * Math.Min(Window.window.Size.X, Window.window.Size.Y)),

        FillColor = Color.Yellow,
        OutlineColor = Color.Red,
        OutlineThickness = 1
    };

    public Vector2f Direction = new Vector2f();

    public int Damage = 1;

    public Vector2f Center()
    {
        return new Vector2f(Shape.Position.X + Shape.Radius, Shape.Position.Y + Shape.Radius);
    }
}

public static class Bullets
{
    public static List<Bullet> bullets = new List<Bullet>();

    public static int Speed = 1;

    private static bool Collision(Bullet bullet)
    {
        bool CollisionWithEnemy = false;

        for (int i = 0; i < Enemies.enemies.Count; i++)
        {
            Vector2f bp = bullet.Center();
            Vector2f ep = Enemies.enemies[i].Center();
            float br = bullet.Shape.Radius;
            float er = Enemies.enemies[i].Shape.Radius;

            if (br + er > Vector.Length(bp, ep))
            {
                CollisionWithEnemy = true;

                if (Enemies.Hit(i, bullet.Damage))
                {
                    i--;
                }

                break;
            }
        }

        return
            CollisionWithEnemy ||
            bullet.Shape.Position.X + 2 * bullet.Shape.Radius < 0 ||
            bullet.Shape.Position.Y + 2 * bullet.Shape.Radius < 0 ||
            bullet.Shape.Position.X > Window.window.Size.X ||
            bullet.Shape.Position.Y > Window.window.Size.Y;
    }

    public static void Move()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (Collision(bullets[i]))
            {
                bullets.RemoveAt(i);
                i--;
            }
            else
            {
                bullets[i].Shape.Position += Speed * bullets[i].Direction;
            }
        }
    }

    public static void New()
    {
        bullets.Add(new Bullet() { Direction = Line.Direction });
    }

    public static void Draw()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            Window.window.Draw(bullets[i].Shape);
        }
    }
}

public static class Line
{
    public static Vector2f Direction = new Vector2f();
    public static Vector2f Point1 = new Vector2f();
    public static Vector2f Point2 = new Vector2f();

    private static void FindDirection()
    {
        float d = Vector.Length(Point1, Point2);
        float l = Vector.Length(new Vector2f(Window.window.Size.X, Window.window.Size.Y)) / 2;
        Vector2f u = Vector.Normalize(Point2 - Point1);

        Point2 += l * u;
        Direction = u;
    }

    public static void Draw()
    {
        Point1 = Window.Center();
        Point2 = new Vector2f(Mouse.GetPosition(Window.window).X, Mouse.GetPosition(Window.window).Y);

        FindDirection();

        VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
        line.Append(new Vertex() { Position = Point1, Color = Color.Red });
        line.Append(new Vertex() { Position = Point2, Color = Color.Red });

        Window.window.Draw(line);
    }
}