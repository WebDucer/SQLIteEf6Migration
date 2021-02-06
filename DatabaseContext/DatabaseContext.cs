using de.webducer.csharp.sqliteef6.BusinessData;
using de.webducer.csharp.sqliteef6.DatabaseContext.Mapping;
using de.webducer.csharp.sqliteef6.DatabaseContext.Migration;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace de.webducer.csharp.sqliteef6.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        private const string _DB_FILE_NAME = @"Data\Time.db";
        private const String _DB_VERSION = "PRAGMA user_version";

        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new CreationionAndMigrationInitializer());
        }

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
        public IDbSet<WorkingTimeRange> WorkingTimes { get; set; }

        private int? _databaseVersion = null;
        /// <summary>
        /// User defined version of the database
        /// </summary>
        public int Version {
            get {
                if (!_databaseVersion.HasValue)
                {
                    _databaseVersion = Database.SqlQuery<int>(_DB_VERSION).Single();
                }
                return _databaseVersion.Value;
            }
            set {
                Database.ExecuteSqlCommand(_DB_VERSION + "=" + value);
                _databaseVersion = null;
            }
        }
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
