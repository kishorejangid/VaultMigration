using System;

namespace VaultMigration
{
    public class UploadServiceEventArgs : EventArgs
    {        
        public int UploadProgress { get; set; }
        public string CurrentUpload { get; set; }
        public int QueueLength { get; set; }
    }
}