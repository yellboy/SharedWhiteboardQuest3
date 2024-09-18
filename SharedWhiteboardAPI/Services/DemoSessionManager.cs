using SharedWhiteboardAPI.Model;
using System.Drawing;
using System.Reflection;
using Newtonsoft.Json;
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
            var filename = $"./Simulation/Board{i:00}.json";

            var whiteboard = JsonConvert.DeserializeObject<Whiteboard>(File.ReadAllText(filename));

            _whiteboards.Add(whiteboard!);
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