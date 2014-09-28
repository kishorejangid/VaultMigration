using System;

namespace VaultMigration
{
    public class DownloadServiceEventArgs : EventArgs
    {
        public int NodeCount { get; set; }
        public int DownloadProgress { get; set; }
        public string CurrentDownload { get; set; }
    }

    public class DownloadProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
    }
}