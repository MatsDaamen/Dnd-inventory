using Dnd_Inventory_API.Dtos.Session.GET;
using Dnd_Inventory_Logic.DomainModels;
using Dnd_Inventory_Logic.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dnd_Inventory_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public List<SessionDTO> Get()
        {
            List<SessionModel> sessionModels = _sessionService.Get();
                
            List<SessionDTO> sessionDTOs = sessionModels.Select(session => new SessionDTO
            {
                Id = session.Id,
                Name = session.Name,
                CreatedBy = session.CreatedBy,
            }).ToList();

            return sessionDTOs;
        }

        [HttpGet("{id}")]
        public SessionDTO Get(int id)
        {
            SessionModel session = _sessionService.Get(id);

            SessionDTO sessionDTO = new SessionDTO
            {
                Id = session.Id,
                Name = session.Name,
                CreatedBy = session.CreatedBy,
            };

            return sessionDTO;
        }

        [HttpPost]
        public void Post(string name, int createdBy) 
        {
            _sessionService.Create(name, createdBy);
        }

        [HttpPost("Join")]
        public void Join(int sessionId, Guid sessionJoinKey, int userId)
        {
            _sessionService.Join(sessionId, sessionJoinKey, userId);
        }

        [HttpPost("JoinKey")]

        public Guid PostJoinKey(int sessionId, int amountOfUses, int createdBy)
        {
            Guid joinKey = _sessionService.CreateJoinKey(sessionId, amountOfUses, createdBy);

            return joinKey;
        }
    }
}
