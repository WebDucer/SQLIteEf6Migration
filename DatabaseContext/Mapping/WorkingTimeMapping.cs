using de.webducer.csharp.sqliteef6.BusinessData;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Mapping {
    public class WorkingTimeMapping : EntityTypeConfiguration<WorkingTimeRange> {
        public WorkingTimeMapping() {
            // Primary key
            this.HasKey(k => k.Id);

            // Nullables
            this.Property(p => p.StartTime).IsRequired();

            // Mapping
            this.ToTable("time_tracking");
            this.Property(p => p.Id).HasColumnName("_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.StartTime).HasColumnName("start_time");
            this.Property(p => p.EndTime).HasColumnName("end_time");
            this.Property(p => p.PauseDuration).HasColumnName("pause_duration");
        }
    }
}
