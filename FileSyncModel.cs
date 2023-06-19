using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;

namespace FileSyncApp
{
    public class FileSyncModel
    {
        private string sourceDirectory;
        private string destinationDirectory;
        private FileSystemWatcher fileWatcher;
        private HashSet<string> processedFiles;
        private XmlDocument xmlLog;
        private List<FileChangeLog> jsonLog;

        public FileSyncModel(string sourceDirectory, string destinationDirectory)
        {
            this.sourceDirectory = sourceDirectory;
            this.destinationDirectory = destinationDirectory;

            InitializeFileWatcher();
            processedFiles = new HashSet<string>();

            xmlLog = new XmlDocument();
            xmlLog.Load("C:\\Forms\\Lab.xml");

            string jsonLogContent = File.ReadAllText("C:\\Forms\\Laba.json");
            jsonLog = JsonConvert.DeserializeObject < List < FileChangeLog >> (jsonLogContent);
        }

        private void InitializeFileWatcher()
        {
            fileWatcher = new FileSystemWatcher(sourceDirectory);
            fileWatcher.IncludeSubdirectories = true;
            fileWatcher.EnableRaisingEvents = true;
            fileWatcher.Created += OnFileCreated;
            fileWatcher.Deleted += OnFileDeleted;
            fileWatcher.Renamed += OnFileRenamed;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            if (!processedFiles.Contains(e.Name))
            {
                string status = $"Файл создан: {e.Name}";
                DisplayStatus(status);

                // изменение в формате XML
                XmlElement logEntry = xmlLog.CreateElement("LogEntry");
                logEntry.SetAttribute("type", "Created");
                logEntry.InnerText = e.Name;
                xmlLog.DocumentElement.AppendChild(logEntry);

                // Log the change in JSON format
                FileChangeLog changeLog = new FileChangeLog()
                {
                    Type = "Created",
                    FileName = e.Name,
                    Timestamp = DateTime.Now
                };
                jsonLog.Add(changeLog);

                processedFiles.Add(e.Name);
            }
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (!processedFiles.Contains(e.Name))
            {
                string status = $"Файл удален: {e.Name}";
                DisplayStatus(status);

                // изменение в формате XML
                XmlElement logEntry = xmlLog.CreateElement("LogEntry");
                logEntry.SetAttribute("type", "Deleted");
                logEntry.InnerText = e.Name;
                xmlLog.DocumentElement.AppendChild(logEntry);

                // Log the change in JSON format
                FileChangeLog changeLog = new FileChangeLog()
                {
                    Type = "Deleted",
                    FileName = e.Name,
                    Timestamp = DateTime.Now
                };
                jsonLog.Add(changeLog);

                processedFiles.Add(e.Name);
            }
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            if (!processedFiles.Contains(e.Name))
            {
                string status = $"Файл переименован: {e.OldName} -> {e.Name}";
                DisplayStatus(status);
                processedFiles.Remove(e.OldName);
                processedFiles.Add(e.Name);

               //изменение в формате XML
                XmlElement logEntry = xmlLog.CreateElement("LogEntry");
                logEntry.SetAttribute("type", "Renamed");
                logEntry.SetAttribute("oldName", e.OldName);
                logEntry.InnerText = e.Name;
                xmlLog.DocumentElement.AppendChild(logEntry);

                //изменение в формате JSON
                FileChangeLog changeLog = new FileChangeLog()
                {
                    Type = "Renamed",
                    OldFileName = e.OldName,
                    NewFileName = e.Name,
                    Timestamp = DateTime.Now
                };
                jsonLog.Add(changeLog);

                processedFiles.Add(e.OldName);
            }
        }

        public void SaveLogsToXml()
        {
            xmlLog.Save("C:\\Forms\\Lab.xml");
        }

        public void SaveLogsToJson()
        {
            string jsonLogContent = JsonConvert.SerializeObject(jsonLog);
            File.WriteAllText("C:\\Forms\\Laba.json", jsonLogContent);
        }

        private void DisplayStatus(string status)
        {
            MessageBox.Show(status);
        }
    }

    public class FileChangeLog
    {
        public string Type { get; set; }
        public string FileName { get; set; }
        public string OldFileName { get; set; }
        public string NewFileName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
