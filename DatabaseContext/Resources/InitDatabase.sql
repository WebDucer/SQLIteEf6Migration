CREATE TABLE IF NOT EXISTS [time_tracking] (
	[_id] INTEGER PRIMARY KEY AUTOINCREMENT,
	[start_time] TEXT NOT NULL,
	[end_time] TEXT,
	[pause_duration] INTEGER NOT NULL DEFAULT 0
);