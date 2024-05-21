using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExplorer.ViewModels
{
    internal class NavigationViewModel
    {
        public DirectoryInfo _Directory { get; set; }

        public NavigationViewModel(DirectoryInfo directory) 
        {
        _Directory = directory;
        


        }


    }
}
