using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tests
{
    public class TestClient
    {
        private readonly IPEndPoint _endPoint;
        private TcpClient _client;

        public TestClient(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        public void Connect()
        {
            _client = new TcpClient();
            _client.Connect(_endPoint);
        }

        public string SendQuery(string query)
        {
            using(_client)
            {
                var stream = _client.GetStream();
                var reader = new StreamReader(stream, new ASCIIEncoding());
                var writer = new StreamWriter(stream, new ASCIIEncoding()) { AutoFlush = true };

                writer.Write(query);

                return reader.ReadToEnd();
            }
        }

        public string SendQueryWithConnect(string query)
        {
            Connect();

            using (_client) {
                var stream = _client.GetStream();
                var reader = new StreamReader(stream, new ASCIIEncoding());
                var writer = new StreamWriter(stream, new ASCIIEncoding()) { AutoFlush = true };

                writer.Write(query);

                return reader.ReadToEnd();
            }
        }
    }
}