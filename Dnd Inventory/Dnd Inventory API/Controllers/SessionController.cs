using Dnd_Inventory_API.Dtos.Session.GET;
using Dnd_Inventory_API.Dtos.Session.JOIN;
using Dnd_Inventory_API.Dtos.Session.JOINKEY;
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
        public List<SessionDTO> Get([FromQuery]string? userId)
        {
            List<SessionModel> sessionModels = _sessionService.Get(userId);
                
            List<SessionDTO> sessionDTOs = sessionModels.Select(session => new SessionDTO
            {
                Id = session.Id,
                Name = session.Name,
                CreatedBy = session.CreatedBy
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
                JoinKeys = session.SessionJoinKeys.Select(joinkey => new JoinKeyDto
                {
                    Id = joinkey.Id,
                    JoinKey = joinkey.JoinKey,
                    UsesLeft = joinkey.UsesLeft,
                    SessionId = session.Id
                }).ToList()
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

        public Guid PostJoinKey([FromBody] JoinKeyRequest joinKeyRequest)
        {
            SessionJoinKeyModel joinKeyModel = new SessionJoinKeyModel
            {
                SessionId = joinKeyRequest.sessionId,
                UsesLeft = joinKeyRequest.amountOfUses,
            };

            Guid joinKey = _sessionService.CreateJoinKey(joinKeyModel, joinKeyRequest.createdBy);

            return joinKey;
        }

        [HttpDelete("joinKey/{guid}")]
        public void DeleteJoinKey(Guid guid)
        {
            _sessionService.DeleteJoinKey(guid);
        }
    }
}
