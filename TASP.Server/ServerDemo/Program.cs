using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TASP.Server;

namespace ServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebServer("127.0.0.1", 13000);

            server.Start();

            var thread = new Thread(() =>
            {
                while (true)
                {

                }
            });

            thread.IsBackground = false;

            thread.Start();
        }
    }
}
