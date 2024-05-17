using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class Line
{
    public static List<Vector2f> Directions = new List<Vector2f>();
    public static double Angle = 0;
    public static int DirectionCount = 16;

    private static void FindMainDirection()
    {
        Vector2f center = Window.Center();
        Vector2f mouse = new Vector2f(Mouse.GetPosition(Window.window).X, Mouse.GetPosition(Window.window).Y);
        Angle = Math.Atan(Vector.Normalize(mouse - center).Y / Vector.Normalize(mouse - center).X);

        //Random random = new Random();
        //Angle = random.Next();

        //Angle += Math.PI / 45;
        //Angle += Angle > 2 * Math.PI ? -2 * Math.PI : 0;
    }

    private static void FindDirections()
    {
        Directions = new List<Vector2f>();

        for (int i = 0; i < DirectionCount; i++)
        {
            Vector2f Direction = new Vector2f();

            Direction.X = (float)Math.Cos(Angle + 2 * Math.PI * i / DirectionCount);
            Direction.Y = (float)Math.Sin(Angle + 2 * Math.PI * i / DirectionCount);

            Directions.Add(Direction);
        }
    }

    public static void Draw()
    {
        float l = Vector.Length(new Vector2f(Window.window.Size.X, Window.window.Size.Y)) / 2;

        FindMainDirection();
        FindDirections();

        for (int i = 0; i < DirectionCount; i++)
        {
            Vector2f Point1 = Window.Center();
            Vector2f Point2 = Point1 + l * Directions[i];

            VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
            line.Append(new Vertex() { Position = Point1, Color = Color.Red });
            line.Append(new Vertex() { Position = Point2, Color = Color.Red });

            Window.window.Draw(line);
        }
    }
}