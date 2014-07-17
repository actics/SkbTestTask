using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class AutocompliteClient
    {
        private readonly IPEndPoint _endPoint;

        public AutocompliteClient(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        public string ComplitePrefix(string prefix)
        {
            using (var client = new TcpClient()) {
                client.Connect(_endPoint);

                var stream = client.GetStream();
                var reader = new StreamReader(stream, new ASCIIEncoding());
                var writer = new StreamWriter(stream, new ASCIIEncoding()) { AutoFlush = true };

                writer.WriteLine("get {0}", prefix);

                return reader.ReadToEnd();
            }
        }
    }
}
