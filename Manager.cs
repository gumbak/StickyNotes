using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyNotes
{
    public class Manager
    {
        private List<Controller> _controllers;
        private Database _database;

        public Manager()
        {
            _controllers = new List<Controller>();
            _database = new StickyNotes.Database();
        }

        private string createId()
        {
            return Guid.NewGuid().ToString();
        }

        public void createNote()
        {
            Controller controller = createNote(createId(), "");
            _controllers.Add(controller);
        }

        private Controller createNote(string id, string content)
        {
            Controller controller = new Controller(id, content, this);
            controller.start();
            return controller;
        }

        public void restoreNotes()
        {
            Controller controller;
            List<MongoDB.Bson.BsonDocument> noteData = _database.getAllNoteData();
            if (noteData == null || noteData.Count == 0)
            {
                createNote();
            }
            else
            {
                foreach (var data in noteData)
                {
                    var id = data.GetValue("id").ToString();
                    var content = data.GetValue("content").ToString();
                    controller = createNote(id, content);
                    _controllers.Add(controller);
                }
            }
        }

        public void deleteNote(Controller controller)
        {
            _database.deleteNote(controller.id);
            controller.exit();
            _controllers.Remove(controller);
        }
    }
}
