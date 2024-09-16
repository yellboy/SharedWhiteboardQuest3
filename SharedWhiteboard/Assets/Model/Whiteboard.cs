using System.Collections.Generic;

namespace Assets.Model
{
    public class Whiteboard
    {
        public IList<Point> ColoredPoints { get; set; } = new List<Point>();
    }
}