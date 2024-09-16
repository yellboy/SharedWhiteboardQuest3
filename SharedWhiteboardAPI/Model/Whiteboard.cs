namespace SharedWhiteboardAPI.Model
{
    public class Whiteboard
    {
        public IList<Point> ColoredPoints { get; set; } = new List<Point>();
    }
}