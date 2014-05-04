using de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SQLite;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Migration {
    public class CreationionAndMigrationInitializer : IDatabaseInitializer<DatabaseContext> {
        #region Required version
        // Minimum version number of the database requiered to work with the programm
        private const int _REQUIRED_VERSION = 1;
        // Default version number bei creating empty databse (DEFAULT: 0)
        private const int _NO_DATABASE_VERSION = 0;
        #endregion

        #region Available migration steps
        // Static migration step for skipping
        private static readonly NullMigrationStep<DatabaseContext> _SKIPP_MIGRATION_STEP = NullMigrationStep<DatabaseContext>.GetInstance();

        // List of all available migration steps. Index represents the version to migrate from to the next
        private static IMigrationStep<DatabaseContext>[] _MIGRATION_STEPS = {
                                                                                new InitDatabaseStep()
                                                                            };
        #endregion

        #region Migration
        public void InitializeDatabase(DatabaseContext context) {
            // get current version
            var currentVersion = context.Version;

            // Check all migration steps to the required version are available
            if(_REQUIRED_VERSION > _MIGRATION_STEPS.Length) {
                throw new IndexOutOfRangeException("Not all migration steps are implemented!");
            }

            if(currentVersion < _REQUIRED_VERSION) {
                // Migration of data and structure

                // Check we have SQLite as databse
                var connection = context.Database.Connection as SQLiteConnection;
                if(connection != null) {
                    // Close prior connection if open
                    if(connection.State == System.Data.ConnectionState.Open) {
                        connection.Close();
                    }

                    // get origin connection string
                    var originConnectionString = connection.ConnectionString;

                    // Create new connection for migration (required to override ForeignKey beahvior of the origin connection)
                    var migrationConnectionString = new SQLiteConnectionStringBuilder(originConnectionString) {
                        ForeignKeys = false
                    }.ToString();

                    // assign the new connection string to the context
                    connection.ConnectionString = migrationConnectionString;

                    // Open connection for migration
                    connection.Open();
                    using(var transaction = connection.BeginTransaction()) {
                        // Migrate structure, before migrating data
                        for(int i = currentVersion; i < _REQUIRED_VERSION; i++) {
                            _MIGRATION_STEPS[i].MigrateStructure(context);
                        }

                        for(int i = currentVersion; i < _REQUIRED_VERSION; i++) {
                            _MIGRATION_STEPS[i].MigrateData(context);
                        }

                        // Set Version to required version
                        context.Version = _REQUIRED_VERSION;

                        // Commit all migration changes to the database
                        transaction.Commit();
                    }

                    // Close migration connection
                    connection.Close();

                    // Set the connection string to the origin value
                    connection.ConnectionString = originConnectionString;
                }
            }
        }
        #endregion
    }
}
