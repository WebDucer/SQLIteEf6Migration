using de.webducer.csharp.sqliteef6.BusinessData;
using de.webducer.csharp.sqliteef6.DatabaseContext.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace de.webducer.csharp.sqliteef6.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        private const string _DB_FILE_NAME = @"Data\Time.db";

        #region Constructors
        public DatabaseContext()
            : this(_DB_FILE_NAME)
        {

        }

        public DatabaseContext(string dbFileName)
            : base(GetConnection(dbFileName), true)
        {

        }
        #endregion

        #region Properties
        public IDbSet<WorkingTimeRange> WorkingTime { get; set; }
        #endregion

        #region Mapping Configuration
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WorkingTimeMapping());
        }
        #endregion

        #region Helper Methods
        private static DbConnection GetConnection(string dbFileName)
        {
            var connectionString = new SQLiteConnectionStringBuilder()
            {
                BinaryGUID = true,
                DataSource = dbFileName,
                DateTimeFormat = SQLiteDateFormats.ISO8601,
                DateTimeKind = DateTimeKind.Local,
                FailIfMissing = false,
                ForeignKeys = true
            }.ToString();

            return new SQLiteConnection(connectionString);
        }
        #endregion
    }
}
