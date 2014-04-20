using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace de.webducer.csharp.sqliteef6.Migration.Tests {
    [TestClass]
    public class CreationDbTest {
        [TestMethod]
        public void DatabseNotExists() {
            using(var context = new DatabaseContext.DatabaseContext(Guid.NewGuid().ToString() + ".db")) {
                context.WorkingTimes.Add(new BusinessData.WorkingTimeRange() { StartTime = DateTime.Now, PauseDuration = 15, EndTime = DateTime.Now.AddHours(7) });
                context.SaveChanges();
                var item = context.WorkingTimes.FirstOrDefault();

                Assert.IsNotNull(item);
            }
        }
    }
}
