using Lab9_2_;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileSyncApp
{
    public class FileSyncPresenter
    {
        private Form1 view;
        private FileSyncModel model;

        public FileSyncPresenter(Form1 view, FileSyncModel model)
        {
            this.view = view;
            this.model = model;

            view.SyncRequested += OnSyncRequested;
            view.FormClosing += OnFormClosing;


        }

        private void OnSyncRequested(object sender, EventArgs e)
        {
            
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            model.SaveLogsToXml();
            model.SaveLogsToJson();
        }
    }
}
