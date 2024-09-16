using SharedWhiteboardAPI.Model;

namespace SharedWhiteboardAPI.Services
{
    public interface ISessionManager
    {
        public string CreateSession();

        public string CreateSessionParticipant(string sessionId);

        public IEnumerable<Whiteboard> GetWhiteboardsOfOtherParticipants(string sessionId, string participantId);

        public void UpdateSessionWhiteboard(string sessionId, string participantId, Whiteboard whiteboard);
    }
}
