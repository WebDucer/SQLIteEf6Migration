﻿using System;

namespace de.webducer.csharp.sqliteef6.BusinessData
{
    public class WorkingTimeRange
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PauseDuration { get; set; }
        public string Comment { get; set; }
    }
}
