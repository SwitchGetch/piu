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