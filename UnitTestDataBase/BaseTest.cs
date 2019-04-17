using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDataBase
{
    [TestClass]
    public class BaseTest
    {
        /// <summary>
        /// Метод используется в качестве конструктора
        /// </summary>
        [AssemblyInitialize]
        public static void initialize(TestContext context)
        {
            /// <summary>
            /// Строка подключения
            /// </summary>
            string connectionString;
            // вставляю в конфигурационный файл абсолютный путь
            // так как база у нас для облегчения тестирования не разворачивается в SQL Server
            // мы должны как то построить путь до нее
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string conn = config.ConnectionStrings.ConnectionStrings["DataContent"].ConnectionString;
            if (conn.Contains("|DataDirectory|"))
            {
                conn = conn.Replace("|DataDirectory|",
                    Path.Combine(
                        Path.GetDirectoryName(typeof(HeadingTest).Assembly.Location),
                        "App_Data"));
            }

            connectionString = conn;
            config.ConnectionStrings.ConnectionStrings["DataContent"].ConnectionString = conn;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
