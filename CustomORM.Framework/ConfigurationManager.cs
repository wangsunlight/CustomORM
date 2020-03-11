using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CustomORM.Framework
{
    //连接字符串
    public class ConfigurationManager
    {
        private static string _SqlConnectionStringCustom = null;
        private static string _SqlConnectionStringWrite = null;
        private static string[] _SqlConnectionStringRead = null;
        private static bool EnableWriteRead = false;
        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionStringCustom = configuration["ConnectionStrings:Customers"];
            _SqlConnectionStringWrite = configuration["ConnectionStrings:Write"];
            _SqlConnectionStringRead = configuration.GetSection("ConnectionStrings").GetSection("Read").GetChildren().Select(s => s.Value).ToArray();

            EnableWriteRead = bool.Parse(configuration["EnableWriteRead"]);
        }

        public static string SqlConnectionStringCustom
        {
            get
            {
                return _SqlConnectionStringCustom;
            }
        }

        public static string SqlConnectionStringWrite
        {
            get
            {
                return _SqlConnectionStringWrite;
            }
        }

        public static string[] SqlConnectionStringRead
        {
            get
            {
                return _SqlConnectionStringRead;
            }
        }

        public static bool GetEnableWriteRead
        {
            get
            {
                return EnableWriteRead;
            }
        }
    }
}
