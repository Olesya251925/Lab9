using System;

namespace FileSyncApp
{
    public interface IFileSyncView
    {
        event EventHandler SyncRequested;
        void DisplayStatus(string status);
    }
}
