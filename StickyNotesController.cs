using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StickyNotes
{
    class StickyNotesController
    {
        private int SAVE_DELAY = 6000;
        private Thread delayedSaveThread;
        private Object _delayedSaveLock = new Object();

        public void triggerDelayedSave()
        {
            lock (_delayedSaveLock)
            {
                if (delayedSaveThread == null)
                {
                    delayedSaveThread = new Thread(this.doDelayedSave);
                    delayedSaveThread.Start();
                }
            }
            
        }

        private void doDelayedSave()
        {
            Thread.Sleep(SAVE_DELAY);
            save();
            lock (_delayedSaveLock)
            {
                delayedSaveThread = null;
            }
        }

        private void save()
        {

        }
    }    
}
