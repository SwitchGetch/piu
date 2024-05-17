using SFML.Graphics;
using SFML.System;

public static class Player
{
    public static int MaxHP = 100;
    public static int CurrentHP = 100;

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
        CurrentHP -= Damage;

        if (CurrentHP <= 0)
        {
            Window.window.Close();
        }
    }
}