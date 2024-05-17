using SFML.Window;
using SFML.Graphics;
using SFML.System;

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
            window.DispatchEvents();

            NextFrame();
        }
    }

    private static void NextFrame()
    {
        window.Clear(new Color(55, 186, 230));

        Line.Draw();

        Bullets.New();
        Bullets.Move();
        Bullets.Draw();

        //Enemies.New();
        //Enemies.Move();
        //Enemies.Draw();

        Player.Draw();

        LifeBar.Draw();

        Text.Draw();

        window.Display();
    }

    public static Vector2f Center(double Radius = 0)
    {
        return new Vector2f(window.Size.X / 2 - (float)Radius, window.Size.Y / 2 - (float)Radius);
    }
}
