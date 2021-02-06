using System.Data.Entity;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces
{
    public interface IMigrationStep<T> where T : DbContext
    {
        void MigrateStructure(T context);

        void MigrateData(T context);
    }
}
