using SharedWhiteboardAPI.Model;

namespace SharedWhiteboardAPI.Services;

public class WhiteboardMerger : IWhiteboardMerger
{
    public Whiteboard Merge(IEnumerable<Whiteboard> whiteboards)
    {
        return new Whiteboard
        {
            ColoredPoints = whiteboards
                .SelectMany(w => w.ColoredPoints)
                .DistinctBy(point => (point.X, point.Y))
                .ToList()
        };
    }
}