using SFML.System;

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