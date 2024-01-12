using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;

namespace Tests
{
    [TestClass]
    public class SessionTests
    {
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

            ISessionService _sessionService = new SessionService(sessionStub.Object,joinKeyStub.Object, sessionUserStub.Object);

            SessionJoinKeyModel joinKey = new SessionJoinKeyModel
            {
                SessionId = 1,
                UsesLeft = 1,
                JoinKey = Guid.NewGuid()
            };

            Assert.ThrowsException<Exception>(() => _sessionService.CreateJoinKey(joinKey, "10"));
        }

        [TestMethod]
        public void TestIfUserIdFilterOnSessionGetAll()
        {
            var sessionStub = new Mock<ISessionRepository>();
            var sessionUserStub = new Mock<ISessionUsersRepository>();
            var joinKeyStub = new Mock<IJoinKeyRepository>();

            string userId = "userId";

            List<SessionModel> expectedSessions = new List<SessionModel> {
                new SessionModel(4, "test 4", userId),
                new SessionModel(5, "test 5", userId),
                new SessionModel(6, "test 6", userId),
            };
            sessionStub.Setup(x => x.GetAll(userId)).Returns(expectedSessions);
            sessionStub.Setup(x => x.GetAll()).Returns(new List<SessionModel> {
                new SessionModel(1, "test 1", "notUserId"),
                new SessionModel(2, "test 2", "alsoNotUser"),
                new SessionModel(3, "test 3", "NotUserAgain")
            });

            ISessionService _sessionService = new SessionService(sessionStub.Object, joinKeyStub.Object, sessionUserStub.Object);


            List<SessionModel> sessionModels = _sessionService.Get(userId);

            Assert.AreEqual(expectedSessions, sessionModels);
        }
    }
}