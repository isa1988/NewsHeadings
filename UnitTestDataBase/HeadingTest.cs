using System;
using System.Collections.Generic;
using System.Configuration;
using DataBase.DataModel;
using DataBase.Media;
using DataBase.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace UnitTestDataBase
{
    [TestClass]
    public class HeadingTest
    {
        [TestMethod]
        public void GetFullAll()
        {
            var dp = new MainWorker();
            try
            {

                List<HeadingInfo> e1 = dp.Heading.GetAll();
                Console.WriteLine(e1.Count);
                Assert.IsNotNull(e1);
                Assert.AreEqual(1, e1.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /*[TestMethod]
        public void CheckXML()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "App.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            MediaFolderConfigSection section = (MediaFolderConfigSection) cfg.Sections["MediaFolder"];
            if (section != null)
            {
                Assert.AreEqual(@"..\NewsHeadingsWeb\Media\", section.FolderItems[0].Path);
            }
        }*/
    }
}
