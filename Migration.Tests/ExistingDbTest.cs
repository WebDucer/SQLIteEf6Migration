using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace de.webducer.csharp.sqliteef6.Migration.Tests
{
    [TestClass]
    [DeploymentItem(@"x86\SQLite.Interop.dll", "x86")]
    [DeploymentItem(@"x64\SQLite.Interop.dll", "x64")]
    [DeploymentItem(@"System.Data.SQLite.EF6.dll", "")]
    [DeploymentItem(@"System.Data.SQLite.Linq.dll", "")]
    public class ExistingDbTest
    {
        [TestMethod]
        [DeploymentItem(@"TestDatabases\testDbV1.db", "TestData")]
        public void DatabaseExistsTest()
        {
            // Arrange
            var dbName = @"TestData\testDbV1.db";
            var newId = 0L;
            using (var context = new DatabaseContext.DatabaseContext(dbName))
            {
                var newItem = context.WorkingTimes.Add(new BusinessData.WorkingTimeRange() { StartTime = DateTime.Now, PauseDuration = 30, EndTime = DateTime.Now.AddHours(7), Comment = "Kommentar 2" });
                context.SaveChanges();
                newId = newItem.Id;
            }

            // Act
            using (var context = new DatabaseContext.DatabaseContext(dbName))
            {
                var oldItem = context.WorkingTimes.Find(1);
                var newItem = context.WorkingTimes.Find(newId);

                // Assert
                Assert.IsNotNull(oldItem);
                Assert.AreEqual(15, oldItem.PauseDuration);
                Assert.IsNull(oldItem.Comment);

                Assert.IsNotNull(newItem);
                Assert.AreEqual(30, newItem.PauseDuration);
                Assert.AreEqual("Kommentar 2", newItem.Comment);
            }
        }
    }
}
