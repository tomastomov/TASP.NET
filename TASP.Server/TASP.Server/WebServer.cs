using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TASP.Server
{
    public class WebServer : IServer
    {
        private readonly HttpListener _listener;

        public WebServer(string ip, int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:13000/");
        }

        public void Start()
        {
            _listener.Start();

            var listenerThread = new Thread(async () => await StartListetingForClients().ConfigureAwait(false));

            listenerThread.Start();
        }

        private async Task StartListetingForClients()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for client..");
                    var clientContext = await _listener.GetContextAsync().ConfigureAwait(false);
                    Console.WriteLine($"Client connected: {clientContext}");

                    Task.Run(async () => await HandleClient(clientContext).ConfigureAwait(false));

                    Console.WriteLine("Thread Started");
                }
            }
            catch (Exception e)
            {
            }
        }

        private async Task HandleClient(HttpListenerContext client)
        {
            Console.WriteLine(client.Request.HttpMethod);
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}
