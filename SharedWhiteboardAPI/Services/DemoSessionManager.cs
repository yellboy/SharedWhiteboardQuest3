using SharedWhiteboardAPI.Model;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using System.Resources;
using Point = SharedWhiteboardAPI.Model.Point;

namespace SharedWhiteboardAPI.Services;

public class DemoSessionManager : SessionManager
{
    private readonly IList<Whiteboard> _whiteboards;

    private static int _currentWhiteboardIndex;

    public DemoSessionManager()
    {
        _whiteboards = new List<Whiteboard>();
        InitializeWhiteboards();
    }

    private void InitializeWhiteboards()
    {
        for (var i = 49; i >= 0; i--)
        {
            var filename = $"SharedWhiteboardAPI.Simulation.Board{i:00}.png";

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
            
            if (stream == null)
            {
                throw new FileNotFoundException("Embedded resource not found.");
            }

            var bitmap = new Bitmap(stream);
            var coloredPixels = new List<Point>();
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        coloredPixels.Add(new Point { X = x, Y = bitmap.Height - y });
                    }
                }
            }

            _whiteboards.Add(new Whiteboard
            {
                ColoredPoints = coloredPixels
            });
        }
    }

    public override IEnumerable<Whiteboard> GetWhiteboardsOfOtherParticipants(string sessionId, string participantId)
    {
        var whiteboard = _whiteboards[_currentWhiteboardIndex++];
        if (_currentWhiteboardIndex == _whiteboards.Count)
        {
            _currentWhiteboardIndex = 0;
        }

        return new List<Whiteboard> { whiteboard };
    }
}