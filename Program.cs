using System;
using System.Windows.Forms;
using FileSyncApp;
using Lab9_2_;

namespace FileSyncApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sourceDirectory = "C:\\Forms";
            string destinationDirectory = "F:\\Forms1";

            var view = new Form1();
            var model = new FileSyncModel(sourceDirectory, destinationDirectory);
            var presenter = new FileSyncPresenter(view, model);

            Application.Run(view);
        }
    }
}
