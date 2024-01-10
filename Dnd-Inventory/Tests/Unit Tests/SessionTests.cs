using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;

namespace Tests
{
    [TestClass]
    public class SessionTests
    {
        private ISessionService _sessionService;

        [TestInitialize]
        public void initialization()
        {
            
        }

        [TestMethod]
        public void TestJoinKeyIsNotMadeBySessionOwner()
        {
            var sessionStub = new Mock<ISessionRepository>();
            var sessionUserStub = new Mock<ISessionUsersRepository>();
            var joinKeyStub = new Mock<IJoinKeyRepository>();

            sessionStub.Setup(x => x.Get(1)).Returns(new SessionModel(1, "test", "1"));

            _sessionService = new SessionService(sessionStub.Object,joinKeyStub.Object, sessionUserStub.Object);

            SessionJoinKeyModel joinKey = new SessionJoinKeyModel
            {
                SessionId = 1,
                UsesLeft = 1,
                JoinKey = Guid.NewGuid()
            };

            Assert.ThrowsException<Exception>(() => _sessionService.CreateJoinKey(joinKey, "10"));
        }
    }
}