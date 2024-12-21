using System;
using System.Net.Sockets;
using System.Text;

class CurrencyClient
{
    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 5000);
            Console.WriteLine("Підключено до сервера.");

            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write("Введіть запит (наприклад, USD EURO або exit): ");
                string request = Console.ReadLine();

                if (request.ToLower() == "exit")
                {
                    Console.WriteLine("Відключення від сервера.");
                    break;
                }

                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                stream.Write(requestBytes, 0, requestBytes.Length);

                byte[] responseBytes = new byte[1024];
                int bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
                string response = Encoding.UTF8.GetString(responseBytes, 0, bytesRead);

                Console.WriteLine($"Відповідь сервера: {response}");
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
