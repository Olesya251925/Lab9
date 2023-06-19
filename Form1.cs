using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;

namespace FileSyncApp
{
    public partial class Form1 : Form
    {
        private string sourceFilePath = "C:\\Forms\\olesya.txt"; // Путь к исходному файлу
        private TextBox resultTextBox = new TextBox();

        public event EventHandler SyncRequested;

        public Form1()
        {
            InitializeComponent();
            SyncRequested += Form1_SyncRequested;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Form1_SyncRequested(object sender, EventArgs e)
        {
            try
            {
                string destinationDirectory = "C:\\Forms"; // Папка назначения

                // Создание пути имен файлов XML и JSON
                string fileName = Path.GetFileName(sourceFilePath);
                string xmlFilePath = Path.Combine(destinationDirectory, $"{fileName}.xml");
                string jsonFilePath = Path.Combine(destinationDirectory, $"{fileName}.json");


                // Создание XML-файла
                using (XmlTextWriter xmlWriter = new XmlTextWriter(xmlFilePath, null))
                {
                    xmlWriter.Formatting = System.Xml.Formatting.Indented;

                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Data");

                    xmlWriter.WriteElementString("FileName", fileName);
                    xmlWriter.WriteElementString("Timestamp", DateTime.Now.ToString());

                    // Дополнительные данные, которые вы хотите сохранить в XML

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }

                // Создание JSON-файла
                string jsonData = JsonConvert.SerializeObject(new { FileName = fileName, Timestamp = DateTime.Now });
                File.WriteAllText(jsonFilePath, jsonData);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при синхронизации и создании файлов: {ex.Message}");
            }
        }

        private void syncButton_Click(object sender, EventArgs e)
        { 
            SyncRequested?.Invoke(this, EventArgs.Empty);

            // Вывод сообщения о начале синхронизации
            statusTextBox.Text = "Синхронизация началась...";

        }

        private void DisplayStatus(string status)
        {
            resultTextBox.AppendText(status + Environment.NewLine);
        }
    }
}
