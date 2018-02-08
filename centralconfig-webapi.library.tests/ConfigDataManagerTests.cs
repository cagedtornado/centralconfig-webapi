using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using centralconfig_webapi.library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace centralconfig_webapi.library.tests
{
    [TestClass]
    public class ConfigDataManagerTests
    {
        /// <summary>
        /// Our test data
        /// </summary>
        private List<configitem> data = new List<configitem>
        {
            new configitem { id = 1, application = "*", name = "Environment", machine = "", value = "DEV", updated = DateTime.Now.AddSeconds(-12) },
            new configitem { id = 2, application = "TestApp", name = "Environment", machine = "", value = "UNITTEST", updated = DateTime.Now.AddSeconds(-1) },
            new configitem { id = 3, application = "TestApp", name = "AnotherItem", machine = "", value = "Some test here", updated = DateTime.Now.AddSeconds(-7) },
            new configitem { id = 4, application = "*", name = "SomeGlobalSetting", machine = "", value = "SettingValue here", updated = DateTime.Now.AddSeconds(-12) },
            new configitem { id = 5, application = "SomeOtherApp", name = "SpecificConfig1", machine = "Machine1", value = "Something very specific", updated = DateTime.Now.AddSeconds(-6) },
            new configitem { id = 6, application = "SomeOtherApp", name = "SpecificConfig1", machine = "", value = "Something somewhat specific", updated = DateTime.Now.AddSeconds(-12) },
            new configitem { id = 7, application = "SomeOtherApp", name = "AnotherConfig", machine = "", value = "Some other value", updated = DateTime.Now.AddSeconds(-8) },
            new configitem { id = 8, application = "SomeOtherApp", name = "Environment", machine = "", value = "SPECIFIC", updated = DateTime.Now.AddSeconds(-12) },
            new configitem { id = 9, application = "SomeOtherApp", name = "Environment", machine = "Machine1", value = "MACHINESPECIFIC", updated = DateTime.Now.AddSeconds(-9) },
        };

        /// <summary>
        /// Our returned data
        /// </summary>
        private IQueryable<configitem> qdata = null;

        /// <summary>
        /// Our mocked dbset
        /// </summary>
        private Mock<DbSet<configitem>> mockSet = new Mock<DbSet<configitem>>();

        /// <summary>
        /// Our mocked database context
        /// </summary>
        private Mock<CentralConfigDb> mockContext = new Mock<CentralConfigDb>();

        [TestInitialize]
        public void Test_Setup()
        {
            //  Our queryable test data
            qdata = new List<configitem>(data).AsQueryable();

            //  Wire up the DBSet operations
            mockSet.As<IQueryable<configitem>>().Setup(m => m.Provider).Returns(qdata.Provider);
            mockSet.As<IQueryable<configitem>>().Setup(m => m.Expression).Returns(qdata.Expression);
            mockSet.As<IQueryable<configitem>>().Setup(m => m.ElementType).Returns(qdata.ElementType);
            mockSet.As<IQueryable<configitem>>().Setup(m => m.GetEnumerator()).Returns(qdata.GetEnumerator());
            
            //  Setup the database context
            mockContext.Setup(c => c.configitems).Returns(mockSet.Object);
        }

        [TestMethod]
        public void Get_ItemDoesntExist_EmptyResults()
        {
            //  Arrange            
            ConfigItem request = new ConfigItem { Application = "BogusApp", Name = "AnotherItem" };
            ConfigDataManager manager = new ConfigDataManager(mockContext.Object);

            //  Act
            var result = manager.Get(request);

            //  Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Id);      //  Should be 0 - we didn't find anything
            Assert.AreEqual("", result.Name);   //  This stuff is blank
            Assert.AreEqual("", result.Value);
        }

        [TestMethod]
        public void GetAllApplications_ReturnsApplications()
        {
            //  Arrange
            ConfigDataManager manager = new ConfigDataManager(mockContext.Object);

            //  Act
            var result = manager.GetAllApplications();

            //  Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Remove_ValidConfigItem_Successful()
        {
            //  Arrange
            ConfigDataManager manager = new ConfigDataManager(mockContext.Object);

            //  Act            
            manager.Remove(new ConfigItem { Application = "SomeOtherApp", Name = "SpecificConfig1" });

            //  Assert
            mockSet.Verify(m => m.Remove(It.IsAny<configitem>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Set_NewConfigItem_SuccessfullyAdds()
        {
            //  Arrange            
            ConfigDataManager manager = new ConfigDataManager(mockContext.Object);

            ConfigItem newItem = new ConfigItem
            {
                Application = "SomeOtherApp",
                Name = "UnitTestItem",
                Value = "UnitTestValue"
            };

            //  Act
            var retval = manager.Set(newItem);

            //  Assert
            mockSet.Verify(m => m.Add(It.IsAny<configitem>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
