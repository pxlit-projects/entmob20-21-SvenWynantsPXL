using System;
using NUnit.Framework;
using SmartHouseLights.Services;
using SmartHouseLights.Services.Interfaces;

namespace SmartHouseLights.Tests.Services
{
    [TestFixture]
    public class ConnectionFactoryTests
    {
        private IConnectionFactory _connectionFactory;

        [SetUp]
        public void Setup()
        {
            _connectionFactory = new ConnectionFactory();
        }

        [Test]
        public void GetHttpClientShouldCreateNewHttpClientWithNoHeaderIfAuthHeaderIsEmpty()
        {
            _connectionFactory.RemoveHeader();
            var client = _connectionFactory.GetHttpClient();

            Assert.That(client, Is.Not.Null);
            Assert.That(client.DefaultRequestHeaders, Is.Empty);
        }

        [Test]
        public void GetHttpClientShouldAddHeaderIfSet()
        {
            string header = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("sven:pxl"));
            _connectionFactory.SetAuthenticationHeader(header);

            var client = _connectionFactory.GetHttpClient();

            Assert.That(client, Is.Not.Null);
            Assert.That(client.DefaultRequestHeaders, Is.Not.Empty);
        }

        [Test]
        public void RemoveHeaderShouldRemoveCredentials()
        {
            string header = "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("sven:pxl"));
            _connectionFactory.SetAuthenticationHeader(header);
            
            var clientWithHeader = _connectionFactory.GetHttpClient();
            
            _connectionFactory.RemoveHeader();

            var clientWithoutHeader = _connectionFactory.GetHttpClient();

            Assert.That(clientWithHeader.DefaultRequestHeaders, Is.Not.Empty);
            Assert.That(clientWithoutHeader.DefaultRequestHeaders, Is.Empty);
        }
    }
}