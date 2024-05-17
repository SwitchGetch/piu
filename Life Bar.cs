using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class LifeBar
{
    private static void Draw(CircleShape Shape, float MaxHP, float CurrentHP)
    {
        float Ratio = CurrentHP / MaxHP;
        byte r = Convert.ToByte(255 * (1 - Ratio));
        byte g = Convert.ToByte(255 * Ratio);
        byte b = 0;

        RectangleShape EmptyBar = new RectangleShape()
        {
            Size = Shape.Radius * new Vector2f(2, 0.5f),
            Position = new Vector2f(Shape.Position.X, Shape.Position.Y - Shape.Radius),
            FillColor = Color.Black,
        };

        RectangleShape FullBar = new RectangleShape()
        {
            Size = Shape.Radius * new Vector2f(2 * Ratio, 0.5f),
            Position = new Vector2f(Shape.Position.X, Shape.Position.Y - Shape.Radius),
            FillColor = new Color(r, g, b),
        };

        if (EmptyBar.Size.X > FullBar.Size.X)
        {
            Window.window.Draw(EmptyBar);
            Window.window.Draw(FullBar);
        }
    }

    public static void Draw()
    {
        Draw(Player.Shape, Player.MaxHP, Player.CurrentHP);

        for (int i = 0; i < Enemies.enemies.Count; i++)
        {
            Draw(Enemies.enemies[i].Shape, Enemies.enemies[i].MaxHP, Enemies.enemies[i].CurrentHP);
        }
    }
}