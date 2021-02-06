using de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces;
using System.Data.Entity;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Migration
{
    public class NullMigrationStep<T> : IMigrationStep<T> where T : DbContext
    {
        private NullMigrationStep() { }

        public void MigrateStructure(T context)
        {
            // Do nothing, skipp
        }

        public void MigrateData(T context)
        {
            // Do nothing, skipp
        }

        public static NullMigrationStep<T> GetInstance()
        {
            return new NullMigrationStep<T>();
        }
    }
}
