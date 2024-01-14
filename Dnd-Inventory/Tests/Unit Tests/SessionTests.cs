using Dnd_Inventory_Logic.Exceptions;
using Dnd_Inventory_Logic.Interfaces.Services;
using Dnd_Inventory_Logic.Services;
using Moq;

namespace Tests.Unit_Tests
{
    [TestClass]
    public class SessionTests
    {
        [TestInitialize]
        public void initialization()
        {
            
        }

        [TestMethod]
        public void TestGetCollection()
        {
            var sessionStub = new Mock<ISessionRepository>();
            var sessionUserStub = new Mock<ISessionUsersRepository>();
            var joinKeyStub = new Mock<IJoinKeyRepository>();

            int sessionId = 1;

            SessionModel expectedSession = new SessionModel(sessionId, "test", "userId")
            {
                SessionJoinKeys = new List<SessionJoinKeyModel>
                {
                    new SessionJoinKeyModel
                    {
                        Id = 1,
                        SessionId = sessionId,
                        JoinKey = new Guid()
                    },
                    new SessionJoinKeyModel
                    {
                        Id = 2,
                        SessionId = sessionId,
                        JoinKey = new Guid()
                    }
                },
                SessionUsers = new List<SessionUserModels>
                {
                    new SessionUserModels
                    {
                        SessionId = sessionId,
                        UserId = "user 1"
                    },
                    new SessionUserModels
                    {
                        SessionId = sessionId,
                        UserId = "user 2"
                    }
                }
            };
            
            sessionStub.Setup(x => x.Get(sessionId)).Returns(expectedSession);
            joinKeyStub.Setup(x => x.GetAllJoinKeys(sessionId)).Returns(expectedSession.SessionJoinKeys);
            sessionUserStub.Setup(x => x.GetAllBySessionId(sessionId)).Returns(expectedSession.SessionUsers);


            ISessionService _sessionService = new SessionService(sessionStub.Object, joinKeyStub.Object, sessionUserStub.Object);


            SessionModel sessionModel = _sessionService.Get(sessionId);

            Assert.AreEqual(expectedSession, sessionModel);
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

            Assert.ThrowsException<JoinKeyCreationExecption>(() => _sessionService.CreateJoinKey(joinKey, "10"));
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