using de.webducer.csharp.sqliteef6.BusinessData;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace de.webducer.csharp.sqliteef6.DatabaseContext.Mapping
{
    public class WorkingTimeMapping : EntityTypeConfiguration<WorkingTimeRange>
    {
        public WorkingTimeMapping()
        {
            // Primary key
            HasKey(k => k.Id);

            // Nullables
            Property(p => p.StartTime).IsRequired();

            // Mapping
            ToTable("time_tracking");
            Property(p => p.Id).HasColumnName("_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.StartTime).HasColumnName("start_time");
            Property(p => p.EndTime).HasColumnName("end_time");
            Property(p => p.PauseDuration).HasColumnName("pause_duration");
            Property(p => p.Comment).HasColumnName("comment");
        }
    }
}
