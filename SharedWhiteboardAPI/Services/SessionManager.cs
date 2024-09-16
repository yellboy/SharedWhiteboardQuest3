using SharedWhiteboardAPI.Model;

namespace SharedWhiteboardAPI.Services;

public class SessionManager : ISessionManager
{
    private readonly IDictionary<string, Session> _sessions = new Dictionary<string, Session>();

    public string CreateSession()
    {
        // TODO Implement properly.
        // For now, we are always using the same session.
        var sessionId = "1";

        _sessions[sessionId] = new Session();

        return sessionId;
    }

    private Session GetSession(string sessionId)
    {
        if (!_sessions.TryGetValue(sessionId, out var session))
        {
            throw new ArgumentException($"There is no session with ID: {sessionId}");
        }

        return session!;
    }

    public string CreateSessionParticipant(string sessionId)
    {
        if (!_sessions.TryGetValue(sessionId, out var session))
        {
            throw new ArgumentException($"There is no session with ID: {sessionId}");
        }

        var participantId = Guid.NewGuid().ToString();
        session.Whiteboards.Add(participantId, new Whiteboard());

        return participantId;
    }

    public virtual IEnumerable<Whiteboard> GetWhiteboardsOfOtherParticipants(string sessionId, string participantId)
    {
        var session = GetSession(sessionId);

        return session.Whiteboards.Where(w => w.Key != participantId).Select(w => w.Value);
    }

    public void UpdateSessionWhiteboard(string sessionId, string participantId, Whiteboard whiteboard)
    {
        var session = GetSession(sessionId);

        if (!session.Whiteboards.ContainsKey(participantId))
        {
            throw new ArgumentException($"There is no participant with ID: {participantId}");
        }

        session.Whiteboards[participantId] = whiteboard;
    }
}