using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StickyNotes
{
    public class Controller
    {
        private Database _database;
        private MainWindow _window;
        private string _id;
        private int SAVE_DELAY = 3000;
        private Thread _delayedSaveThread;
        private Object _delayedSaveLock = new Object();

        public Controller(string id)
        {
            _id = id;
            _database = new StickyNotes.Database();
            _window = new MainWindow(this);       
        }

        public async void start()
        {
            string content = await getContent();
            _window.setContent(content);
        }

        public void triggerDelayedSave()
        {
            lock (_delayedSaveLock)
            {
                if (_delayedSaveThread == null)
                {
                    _delayedSaveThread = new Thread(this.doDelayedSave);
                    _delayedSaveThread.Start();
                }
            }
            
        }

        private void doDelayedSave()
        {
            Thread.Sleep(SAVE_DELAY);
            save();
            lock (_delayedSaveLock)
            {
                _delayedSaveThread = null;
            }
        }

        private void save()
        {
            // TODO: pass content
            _database.save(_id, "temp");
        }

        public async Task<string> getContent()
        {
            string content = await _database.get(_id, ConfigurationManager.AppSettings["DATABASE_FIELD_NAME_CONTENT"]);
            return content;
        }
    }    
}
