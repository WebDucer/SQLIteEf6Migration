using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace de.webducer.csharp.sqliteef6.Migration.Tests
{
    [TestClass]
    public class CreationDbTest
    {
        [TestMethod]
        public void DatabseNotExists()
        {
            // Arrange
            var dbName = $"{Guid.NewGuid()}.db";
            using (var context = new DatabaseContext.DatabaseContext(dbName))
            {
                context.WorkingTimes.Add(new BusinessData.WorkingTimeRange() { StartTime = DateTime.Now, PauseDuration = 15, EndTime = DateTime.Now.AddHours(7), Comment = "Kommentar 1" });
                context.SaveChanges();
            }

            // Act
            using (var context = new DatabaseContext.DatabaseContext(dbName))
            {
                var item = context.WorkingTimes.FirstOrDefault();

                // Assert
                Assert.IsNotNull(item);
                Assert.AreEqual(15, item.PauseDuration);
                Assert.AreEqual("Kommentar 1", item.Comment);
            }
        }
    }
}
