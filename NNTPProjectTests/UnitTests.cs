using NNTPProject;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace NNTPProjectTests
{
    public class Tests
    {
        Client client;
        //NetworkStream networkStream;

        [SetUp]
        public void Setup()
        {
            client = new Client(new User("miklis01@easv365.dk", "e61faa"), "news.dotsrc.org", 119);
        }

        [Test]
        [TestCase("news.dotsrc.org", 119, ExpectedResult = true)]
        [TestCase("news.dotsrc.org", 120, ExpectedResult = false)]
        [TestCase("news.dotssr.org", 119, ExpectedResult = false)]
        public bool Can_Connect_To_Server(string domain, int port)
        {
            return client.CanConnect(domain, port);
        }

        [Test]
        [TestCase(ExpectedResult = true)]
        public bool Can_Login_As_User()
        {
            return client.CanLogin();
        }

        //[Test]
        //public bool Any_Available_News_Sources(string )
        //{
        //    return true;
        //}

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}