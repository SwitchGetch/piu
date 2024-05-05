using SFML.Window;
using SFML.Graphics;
using SFML.System;

public static class Config
{
    public static float BulletRadius = 10;
    public static int BulletDamage = 1;
    public static float BulletSpeed = 1;
    public static int MaxBulletCount = 1;

    public static int PlayerHealth = 10;
    public static int Money = 0;

    public static int MaxScore = 0;
    public static int DifficultyLevel = 1;
}

public static class Vector
{
    public static float Length(Vector2f Point1, Vector2f Point2 = new Vector2f())
    {
        return (float)Math.Sqrt(Math.Pow(Point1.X - Point2.X, 2) + Math.Pow(Point1.Y - Point2.Y, 2));
    }

    public static Vector2f Normalize(Vector2f vector)
    {
        return new Vector2f(vector.X / Length(vector), vector.Y / Length(vector));
    }
}

public static class Event
{
    public static void OnMouseClick(object sender, EventArgs e)
    {
        // check if mouse on any button

        Bullets.New();
    }

    public static void OnClose(object sender, EventArgs e)
    {
        Window.window.Close();
    }
}

public static class Window
{
    public static RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "", Styles.Fullscreen);

    public static void Start()
    {
        window.SetFramerateLimit(60);
        window.MouseButtonPressed += Event.OnMouseClick;
        window.Closed += Event.OnClose;

        while (window.IsOpen)
        {
            NextFrame();

            window.DispatchEvents();
        }
    }

    private static void NextFrame()
    {
        window.Clear(new Color(55, 186, 230));

        Line.Draw();

        Bullets.Move();
        Bullets.Draw();

        Enemies.New();
        Enemies.Move();
        Enemies.Draw();

        Player.Draw();

        window.Display();
    }

    public static Vector2f Center(double Radius = 0)
    {
        return new Vector2f(window.Size.X / 2 - (float)Radius, window.Size.Y / 2 - (float)Radius);
    }
}
