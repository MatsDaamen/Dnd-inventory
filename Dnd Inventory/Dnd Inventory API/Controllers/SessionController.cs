using Dnd_Inventory_API.Dtos.Session.GET;
using Dnd_Inventory_API.Dtos.Session.JOIN;
using Dnd_Inventory_API.Dtos.Session.POST;
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
        public List<SessionDTO> Get([FromQuery]int userId)
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
        public void Post([FromBody]SessionRequest sessionRequest) 
        {
            SessionModel sessionModel = new SessionModel(sessionRequest.name, sessionRequest.createdBy);

            _sessionService.Create(sessionModel);
        }

        [HttpPost("Join")]
        public void Join([FromBody]JoinRequestDto joinRequestDto)
        {
            JoinRequestModel joinRequestModel = new JoinRequestModel
            {
                sessionJoinKey = Guid.Parse(joinRequestDto.sessionJoinKey),
                userId = joinRequestDto.userId
            };

            _sessionService.Join(joinRequestModel);
        }

        [HttpPost("JoinKey")]

        public Guid PostJoinKey(int sessionId, int amountOfUses, int createdBy)
        {
            Guid joinKey = _sessionService.CreateJoinKey(sessionId, amountOfUses, createdBy);

            return joinKey;
        }
    }
}
