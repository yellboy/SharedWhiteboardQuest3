using Microsoft.AspNetCore.Mvc;
using SharedWhiteboardAPI.Services;

namespace SharedWhiteboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionManager _sessionManager;

        public SessionController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        [HttpGet]
        public string CreateSession()
        {
            return _sessionManager.CreateSession();
        }
    }
}
