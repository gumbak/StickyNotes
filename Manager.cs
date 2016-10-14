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

        public void restoreNotes()
        {
            List<MongoDB.Bson.BsonDocument> noteData = _database.getAllNoteData();
            if (noteData == null || noteData.Count == 0)
            {
                createEmptyNote();
            }
            else
            {
                foreach (var data in noteData)
                {
                    var id = data.GetValue("id").ToString();
                    var content = data.GetValue("content").ToString();
                    createNote(id, content);
                }
            }
        }

        private string createId()
        {
            return Guid.NewGuid().ToString();
        }

        public void createEmptyNote()
        {
            createNote(createId(), "");
        }

        private void createNote(string id, string content)
        {
            Controller controller = new Controller(id, content, this);
            _controllers.Add(controller);
            controller.start();
        }

        public void deleteNote(Controller controller)
        {
            _database.deleteNote(controller.id);
            controller.exit();
            _controllers.Remove(controller);
        }
    }
}
