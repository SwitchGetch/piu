using SFML.Graphics;
using SFML.System;

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

    public bool IsSpectral = true;

    public Vector2f Center()
    {
        return new Vector2f(Shape.Position.X + Shape.Radius, Shape.Position.Y + Shape.Radius);
    }
}

public static class Bullets
{
    public static List<Bullet> bullets = new List<Bullet>();

    public static int Speed = 5; 

    private static bool CollisionWithEnemy(Bullet bullet)
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

                if (bullet.IsSpectral == false)
                {
                    break;
                }
            }
        }

        return CollisionWithEnemy;
    }

    private static bool CollisionWithWindow(Bullet bullet)
    {
        return
            bullet.Shape.Position.X + 2 * bullet.Shape.Radius < 0 ||
            bullet.Shape.Position.Y + 2 * bullet.Shape.Radius < 0 ||
            bullet.Shape.Position.X > Window.window.Size.X ||
            bullet.Shape.Position.Y > Window.window.Size.Y;
    }

    public static void Move()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Shape.Position += Speed * bullets[i].Direction;

            if (CollisionWithWindow(bullets[i]) || CollisionWithEnemy(bullets[i]) && bullets[i].IsSpectral == false)
            {
                bullets.RemoveAt(i);
                i--;
            }
        }
    }

    public static void New()
    {
        for (int i = 0; i < Line.DirectionCount; i++)
        {
            Bullet bullet = new Bullet();
            Vector2f Position = Window.Center(bullet.Shape.Radius);
            Vector2f Direction = Line.Directions[i];

            bullet.Shape.Position = Position;
            bullet.Direction = Direction;

            bullets.Add(bullet);
        }
    }

    public static void Draw()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            Window.window.Draw(bullets[i].Shape);
        }
    }
}