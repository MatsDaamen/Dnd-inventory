using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Tests.Mock;

namespace Tests
{
    [TestClass]
    public class SessionTests
    {
        private ISessionService _sessionService;

        [TestInitialize]
        public void initialization()
        {
            SessionRepositoryMock mock = new SessionRepositoryMock();

            mock.Sessions = new List<SessionModel>();
            mock.JoinKeys = new List<SessionJoinKeyModel>();

            _sessionService = new SessionService(mock);
        }

        [TestMethod]
        public void TestJoinKeyIsNotMadeBySessionOwner()
        {
            SessionModel session = new SessionModel(1, "test", 1);

            _sessionService.Create(session);

            SessionJoinKeyModel joinKey = new SessionJoinKeyModel
            {
                SessionId = 1,
                UsesLeft = 1,
                JoinKey = Guid.NewGuid()
            };

            Assert.ThrowsException<Exception>(() => _sessionService.CreateJoinKey(joinKey, 10));
        }
    }
}