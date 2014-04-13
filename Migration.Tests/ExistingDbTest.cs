using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using de.webducer.csharp.sqliteef6.DatabaseContext;
using System.Linq;

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
        [DeploymentItem(@"TestDatabases\testDbV1.db","TestData")]
        public void DatabaseExistsTest()
        {
            using (var context = new DatabaseContext.DatabaseContext(@"TestData\testDbV1.db"))
            {
                context.WorkingTime.Add(new BusinessData.WorkingTimeRange() { StartTime = DateTime.Now, PauseDuration = 15, EndTime = DateTime.Now.AddHours(7) });
                context.SaveChanges();
                var item = context.WorkingTime.FirstOrDefault();

                Assert.IsNotNull(item);
            }
        }
    }
}
