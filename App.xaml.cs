using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Database _database;
        private List<Controller> _controllers;

        public App()
        {
            _controllers = new List<Controller>();
            _database = new StickyNotes.Database();
            restoreNotes();
        }

        private void restoreNotes()
        {
            List<string> noteIds = _database.getAllNoteIds();
            if (noteIds == null || noteIds.Count == 0)
            {
               createNote(createId());
            } else
            {
                
            }
        }

        private void createNote(string id)
        {
            Controller controller = new Controller(id);
            controller.start();
            _controllers.Add(controller);
        }

        private string createId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
