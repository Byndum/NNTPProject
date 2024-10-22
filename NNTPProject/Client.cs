using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NNTPProject
{
    public static class Client
    {
        private static TcpClient _client = new TcpClient();

        public static TcpClient getClient 
        { 
            get { return _client; } 
        }
	}
}
