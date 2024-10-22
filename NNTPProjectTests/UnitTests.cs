using NNTPProject;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace NNTPProjectTests
{
    [TestFixture]
    public class Tests
    {
        TcpClient tcpClient;
        NetworkStream networkStream;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase("news.dotsrc.org", 119, ExpectedResult = 1)]
        public int TestServerAvailability(string domain, int port)
        {
            try
            {
                tcpClient.Connect(domain, port);
                return 1;
            } catch (SocketException ex) 
            {
                return 0;
            }
        }

        [TearDown]
        public void TearDown()
        {
            networkStream.Close();
            tcpClient.Close();
        }
    }
}