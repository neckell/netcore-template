namespace AspNetCoreTemplate.Web.Tests.SqlServer
{
    using System;
    using System.IO;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class SqlConnectionTest
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionTest()
        {
            // Load the configuration from appsettings.json
            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public void Test_Connection_Ok()
        {
            // Arrange
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Act
            bool isConnected = this.TestConnection(connectionString);

            // Assert
            Assert.True(isConnected, "Connection failed.");
        }

        private bool TestConnection(string connectionString)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while testing connection: " + ex.Message);
                return false;
            }
        }
    }

}
