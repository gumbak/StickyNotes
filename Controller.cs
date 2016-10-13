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
        public string id;
        private Database _database;
        private MainWindow _window;
        private Manager _manager;
        private int SAVE_DELAY = 3000;
        private Thread _delayedSaveThread;
        private Object _delayedSaveLock = new Object();

        public Controller(string id, string content, Manager manager)
        {
            this.id = id;
            _database = new StickyNotes.Database();
            _manager = manager;
            _window = new MainWindow(this);
            _window.setContent(content);
        }

        public void createNote()
        {
            _manager.createNote();
        }

        public void deleteNote()
        {
            _manager.deleteNote(this);
        }

        public async void start()
        {
            string content = await getContent();
        }

        public void exit()
        {
            _window.Close();
        }

        public void triggerDelayedSave()
        {
            lock (_delayedSaveLock)
            {
                if (_delayedSaveThread == null)
                {
                    _delayedSaveThread = new Thread(() => doDelayedSave());
                    _delayedSaveThread.Start();
                }
            }
            
        }

        private void doDelayedSave()
        {
            Thread.Sleep(SAVE_DELAY);
            string content = _window.getContent();
            save(content);
            lock (_delayedSaveLock)
            {
                _delayedSaveThread = null;
            }
        }

        private void save(string content)
        {
            _database.save(id, content);
        }        

        public async Task<string> getContent()
        {
            string content = await _database.get(id, ConfigurationManager.AppSettings["DATABASE_FIELD_NAME_CONTENT"]);
            return content;
        }
    }    
}
