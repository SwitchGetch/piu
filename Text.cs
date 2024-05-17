using SFML.Graphics;

public static class Text
{ 
    public static Font font = new Font("./ARLRDBD.TTF");
    public static SFML.Graphics.Text text = new SFML.Graphics.Text("", font, 25);

    public static void Draw()
    {
        string str = "x: " + Line.Directions[0].X + "\ny: " + Line.Directions[0].Y + "\n" + Line.Angle;

        text.DisplayedString = str;

        Window.window.Draw(text);
    }
}