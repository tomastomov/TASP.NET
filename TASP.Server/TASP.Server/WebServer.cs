using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TASP.Server
{
    internal class WebServer : IServer
    {
        private readonly TcpListener _listener;

        public WebServer(string ip, int port)
        {
            var ipAddress = IPAddress.Parse(ip);
            _listener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            _listener.Start();

            StartListetingForClients();
        }

        private async Task StartListetingForClients()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for client..");
                    var client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    Console.WriteLine($"Client connected: {client}");

                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        HandleClient(client);   
                    });
                }
            }
            catch (Exception e)
            {
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            var stream = client.GetStream();

            string data = null;

            var bytes = new byte[256];

            var read = 0;

            while ((read = await stream.ReadAsync(bytes).ConfigureAwait(false)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes);

                Console.WriteLine(data);
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}
