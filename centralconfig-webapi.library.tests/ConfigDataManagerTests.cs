using System;
using centralconfig_webapi.library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace centralconfig_webapi.library.tests
{
    [TestClass]
    public class ConfigDataManagerTests
    {
        [TestMethod]
        public void Get_ItemDoesntExist_EmptyResults()
        {
            //  Arrange
            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);

                //  Act
                var result = manager.Get(new ConfigItem { Application = "Formbuilder", Name = "TestItem" });

                //  Assert
                Assert.IsNotNull(result);
                
            }

        }
    }
}
