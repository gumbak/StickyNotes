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
            List<MongoDB.Bson.BsonDocument> noteData = _database.getAllNoteData();
            if (noteData == null || noteData.Count == 0)
            {
               createNote(createId(), "");
            } else
            {
                foreach (var data in noteData)
                {
                    var id = data.GetValue("id").ToString();
                    var content = data.GetValue("content").ToString();
                      createNote(id, content);
                }
            }
        }

        private void createNote(string id, string content) 
        {
            Controller controller = new Controller(id, content);
            controller.start();
            _controllers.Add(controller);
        }

        private string createId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
