using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AutocompliteServer
    {
        private readonly QueryExecuter _queryExecuter;
        private readonly TcpListener _tcpListener;
        private bool _stopped;

        public AutocompliteServer(int port, QueryExecuter queryExecuter)
        {
            _queryExecuter = queryExecuter;
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _stopped = false;
        }

        public void Forever()
        {
            _tcpListener.Start();
            while (!_stopped)
            {
                var clientSocket = _tcpListener.AcceptSocket();
                Task.Factory.StartNew(() => ClientWork(clientSocket));
            }
        }

        private async void ClientWork(Socket clientSocket)
        {
            using (clientSocket)
            {
                try
                {
                    var stream = new NetworkStream(clientSocket);
                    var reader = new StreamReader(stream, new ASCIIEncoding());

                    var line = await reader.ReadLineAsync();
                    var answer = _queryExecuter.ExecuteQuery(line) ?? "invalid query";

                    var writer = new StreamWriter(stream, new ASCIIEncoding()) {AutoFlush = true};
                    await writer.WriteLineAsync(answer);
                }
                catch (Exception exp)
                {
                    Console.Error.WriteLine(exp.Message);
                }
            }
        }

        public void Stop()
        {
            _stopped = true;
            _tcpListener.Stop();
        }
    }
}
