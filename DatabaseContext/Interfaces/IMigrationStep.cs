using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Interfaces {
    public interface IMigrationStep<T> where T : DbContext {
        void MigrateStructure(T context);

        void MigrateData(T context);
    }
}
