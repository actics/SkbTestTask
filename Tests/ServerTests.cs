using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Server;
using SkbLib;

namespace Tests
{
    [TestFixture]
    class ServerTests
    {
        private const int Port = 1337;
        private AutocompliteServer _server;
        private QueryExecuter _queryExecuter;
        private IPEndPoint _serverEndPoint;
        private TestClient _client;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var searchEngine = SearchEngine.GenerateFromDictionary(TestDictionary.Dictionary);
            _queryExecuter = new QueryExecuter(searchEngine);
            _serverEndPoint = new IPEndPoint(IPAddress.Loopback, Port);
        }

        [SetUp]
        public void SetUp()
        {
            _server = new AutocompliteServer(Port, _queryExecuter);
            _client = new TestClient(_serverEndPoint);

            Task.Factory.StartNew(_server.Forever);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        public void Test1()
        {
            const string correctAnswer = "sakurb\r\nsakurc\r\nsakura\r\n";
            var answer = _client.SendQueryWithConnect("get sak\r\n");

            Assert.IsTrue(correctAnswer == answer);
        }

        [Test]
        public void Test2()
        {
            const string correctAnswer = "\r\n";
            var answer = _client.SendQueryWithConnect("get f\r\n");

            Assert.IsTrue(correctAnswer == answer);
        }

        [Test]
        public void Test3()
        {
            const string correctAnswer = "invalid query\r\n";
            var answer = _client.SendQueryWithConnect("invalid\r\n");

            Assert.IsTrue(correctAnswer == answer);
        }


        [Test]
        public void Test4()
        {
            const int clientCount = 10;
            const string correctAnswer = "sakura\r\n";

            var clients = new TestClient[clientCount];

            for (var i = 0 ; i < clientCount; i++)
            {
                clients[i] = new TestClient(_serverEndPoint);
                clients[i].Connect();
            }

            var accept = true;
            foreach (var client in clients)
            {
                var answer = client.SendQuery("get sakura\r\n");
                if (answer != correctAnswer)
                {
                    accept = false;
                    break;
                }
            }

            Assert.IsTrue(accept);
        }
    }
}
