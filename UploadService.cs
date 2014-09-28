using System;
using System.Collections.Concurrent;
using System.Threading;

namespace VaultMigration
{
    public class UploadService
    {
        public event EventHandler<UploadServiceEventArgs> CurrentUploadChanged;


        public BlockingCollection<VaultObject> Queue { private get; set; }

        public void Upload()
        {
            foreach (var vaultObject in Queue.GetConsumingEnumerable())
            {
                var obj = vaultObject;
                CurrentUploadChanged.Raise(this, new UploadServiceEventArgs { CurrentUpload = obj.Name,QueueLength = Queue.Count });
                Thread.Sleep(300);
            }
        }
    }
}