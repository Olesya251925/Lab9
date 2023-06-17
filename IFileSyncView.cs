using System;

namespace FileSyncApp
{
    // Определение интерфейса IFileSyncView
    public interface IFileSyncView
    {
        // Событие, возникающее при запросе на синхронизацию
        event EventHandler SyncRequested;

        // Метод для отображения статуса синхронизации
        void DisplayStatus(string status);
    }
}

