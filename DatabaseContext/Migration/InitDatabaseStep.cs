using de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Migration
{
    public class InitDatabaseStep : IMigrationStep<DatabaseContext>
    {
        public void MigrateStructure(DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand(Properties.Resources.InitDatabase);
        }

        public void MigrateData(DatabaseContext context)
        {
            // No data changes bei initialization
        }
    }
}
