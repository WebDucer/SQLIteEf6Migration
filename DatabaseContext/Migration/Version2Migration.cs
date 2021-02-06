using de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Migration
{
    public class Version2Migration : IMigrationStep<DatabaseContext>
    {
        public void MigrateData(DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand(Properties.Resources.Version2Migration);
        }

        public void MigrateStructure(DatabaseContext context)
        {
            // No data to migrate
        }
    }
}
