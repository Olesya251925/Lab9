using System;
using System.Windows.Forms;

namespace FileSyncApp
{
    public partial class FileSyncView : Form, IFileSyncView
    {
        private Button syncButton; // Объявление кнопки с именем syncButton
        private TextBox statusTextBox; // Объявление текстового поля с именем statusTextBox

        public event EventHandler SyncRequested;

        public FileSyncView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Инициализация полей syncButton и statusTextBox
            syncButton = new Button();
            statusTextBox = new TextBox();

            // Присваиваем значения свойствам полей
            syncButton.Text = "Синхронизировать";
            statusTextBox.Multiline = true;
            statusTextBox.ScrollBars = ScrollBars.Vertical;

            // Добавляем элементы управления на форму
            Controls.Add(syncButton);
            Controls.Add(statusTextBox);

            // Подписываемся на событие нажатия кнопки
            syncButton.Click += syncButton_Click;
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            // Вызываем событие синхронизации
            SyncRequested?.Invoke(this, EventArgs.Empty);
        }

        public void DisplayStatus(string status)
        {
            // Отображаем информацию в текстовом поле
            statusTextBox.Text += status + Environment.NewLine;
        }
    }
    
    // Определение интерфейса IFileSyncView
    public interface IFileSyncView
    {
        // Событие, возникающее при запросе на синхронизацию
        event EventHandler SyncRequested;

        // Метод для отображения статуса синхронизации
        void DisplayStatus(string status);
    }
}
