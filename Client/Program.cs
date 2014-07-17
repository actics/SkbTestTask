using System;
using System.Net;

namespace Client
{
    internal class Program
    {
        private const int MaxPrefixLength = 15;

        private static IPEndPoint CreateEndPointFromArguments(string[] args)
        {
            if (args.Length != 2)
                throw new ApplicationException(
                    "Клиенту передано неправильное количество аргументов. Необходимо два: IPv4 адрес сервера и порт");

            IPAddress address;
            if (!IPAddress.TryParse(args[0], out address))
                throw new ApplicationException("Неправильный IPv4 адрес");

            UInt16 port;
            if (!UInt16.TryParse(args[1], out port))
                throw new ApplicationException("Неправильный номер порта");

            return new IPEndPoint(address, port);
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Добро пожаловать в skb test client!");
            Console.WriteLine("Для выхода введите в качестве префикса символ '@'");
            Console.WriteLine();
        }

        private static void Main(string[] args)
        {
            PrintWelcomeMessage();

            var remoteEndPoint = CreateEndPointFromArguments(args);
            var client = new AutocompliteClient(remoteEndPoint);

            MainLoop(client);
        }

        private static void MainLoop(AutocompliteClient client)
        {
            while (true)
            {
                Console.Write("Введите префикс: ");

                var prefix = Console.ReadLine();

                if (string.IsNullOrEmpty(prefix))
                    continue;

                if (prefix.StartsWith("@"))
                    break;

                if (prefix.Length > MaxPrefixLength)
                {
                    Console.Error.WriteLine("Невалидный префикс");
                    continue;
                }

                try
                {
                    var response = client.ComplitePrefix(prefix);
                    if (string.IsNullOrWhiteSpace(response))
                        response = "На сервере не найдено слов с данным префиксом";

                    Console.WriteLine(response);
                }
                catch (Exception exp)
                {
                    Console.Error.WriteLine("Ошибка при работе с сервером: {0}", exp.Message);
                }
            }
        }
    }
}