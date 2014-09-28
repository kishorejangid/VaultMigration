using System;

namespace VaultMigration
{
    public static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler<DownloadServiceEventArgs> eventHandler, object sender, DownloadServiceEventArgs eventArgs)
        {
            if (eventHandler != null)
                eventHandler(sender, eventArgs);
        }

        public static void Raise(this EventHandler<UploadServiceEventArgs> eventHandler, object sender, UploadServiceEventArgs eventArgs)
        {
            if (eventHandler != null)
                eventHandler(sender, eventArgs);
        }

        public static void Raise(this EventHandler<DownloadProgressEventArgs> eventHandler, object sender, DownloadProgressEventArgs eventArgs)
        {
            if (eventHandler != null)
                eventHandler(sender, eventArgs);
        }
    }
}