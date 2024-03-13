using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace CognexScannerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SendTelnetCommand("192.168.227.70", 23, "||>SET SYMBOL.QR ON");
            await SendTelnetCommand("192.168.227.70", 23, "||>SET SYMBOL.C128 OFF");
            await SendTelnetCommand("192.168.227.70", 23, "||>SET SYMBOL.C39 OFF");
            await SendTelnetCommand("192.168.227.70", 23, "||>SET SYMBOL.C93 OFF");
        }

        static async Task SendTelnetCommand(string ipAddress, int port, string message)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(ipAddress, port);
                    if (client.Connected)
                    {
                        Console.WriteLine("Połączono z urządzeniem.");
                        var stream = client.GetStream();
                        var writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };

                        await writer.WriteLineAsync(message);
                        Console.WriteLine($"Wysłano wiadomość: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
