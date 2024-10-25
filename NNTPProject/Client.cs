using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics;

namespace NNTPProject
{
    public class Client
    {
        private TcpClient client;
        private User user;
        private NetworkStream stream;
        private string targetDomain;
        private int targetPort;
        private string responseData;

        public Client(User user, string domain, int port) 
        {
            this.client = new TcpClient();
            this.user = user;
            targetDomain = domain;
            targetPort = port;

        }

        public TcpClient GetClient()
        {
            return client;
        }

        public bool CanConnect(string domain, int port)
        {
            try
            {
                client.Connect(domain, port);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CanLogin()
        {
            string response;
            if(!client.Connected)
            {
                client.Connect(targetDomain, targetPort);
            }
            Debug.WriteLine("response after sending username:");
            response = SendCommand($"AUTHINFO USER {user.Username}\r\n");
            Debug.WriteLine(response);

            Debug.WriteLine("response after sending password:");
            response = SendCommand($"AUTHINFO PASS {user.Password}\r\n");
            Debug.WriteLine(response);

            if (response.Equals("281 Ok\r\n"))
            {
                return true;
            }else
            {
                return false;
            }
        }

        public string SendCommand(string command) 
        {
            stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
            
            data = new byte[1024];

            int bytes = stream.Read(data, 0, data.Length);
            return Encoding.ASCII.GetString(data, 0, bytes);

        }
        public void Dispose()
        {
            if (stream != null)
            {
                stream.Close();
            }
            client.Close();
        }
	}
}
