using Microsoft.AspNetCore.Mvc;
using SharedWhiteboardAPI.Model;
using SharedWhiteboardAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharedWhiteboardAPI.Controllers
{
    [Route("api/[controller]")]
    public class WhiteboardController : ControllerBase
    {
        private readonly ISessionManager _sessionManager;
        private readonly IWhiteboardMerger _whiteboardMerger;

        public WhiteboardController(ISessionManager sessionManager, IWhiteboardMerger whiteboardMerger)
        {
            _sessionManager = sessionManager;
            _whiteboardMerger = whiteboardMerger;
        }

        [HttpGet]
        public Whiteboard GetOtherParticipantWhiteboards(string sessionId, string participantId)
        {
            var whiteboards = _sessionManager.GetWhiteboardsOfOtherParticipants(sessionId, participantId);

            var result = _whiteboardMerger.Merge(whiteboards);
            return result;
        }

        [HttpPost]
        public void UpdateWhiteboard(string sessionId, string participantId, [FromBody] Whiteboard whiteboard)
        {
            _sessionManager.UpdateSessionWhiteboard(sessionId, participantId, whiteboard);
        }
    }
}
