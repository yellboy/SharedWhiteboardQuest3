using Microsoft.AspNetCore.Mvc;
using SharedWhiteboardAPI.Services;

namespace SharedWhiteboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly ISessionManager _sessionManager;

        public ParticipantController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public string JoinSession(string sessionId)
        {
            return _sessionManager.CreateSessionParticipant(sessionId);
        }
    }
}
