﻿using System;
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
        private Manager _manager;

        public App()
        {
            _manager = new Manager();
            _manager.restoreNotes();
        }

        

        
    }
}
