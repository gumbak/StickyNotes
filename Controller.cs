using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StickyNotes
{
    class Controller
    {
        private Database _database;
        private int SAVE_DELAY = 3000;
        private Thread _delayedSaveThread;
        private Object _delayedSaveLock = new Object();

        public Controller()
        {
            _database = new StickyNotes.Database();
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
            // TODO: pass ID and content
            _database.save("temp", "temp");
        }

        public async void getContent(string id)
        {
            string content = await _database.get(id, ConfigurationManager.AppSettings["DATABASE_FIELD_NAME_CONTENT"]);
        }
    }    
}
