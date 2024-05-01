using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
///　Modelのインポート
using WindowsExplorer.Models;




namespace WindowsExplorer.ViewModels
{
    class MainWindowViewModel
    {
       public List<Model_TreeViewItem> VM { get; }
        
        /// <summary>
        /// パスの引き渡し
        /// </summary>
       public MainWindowViewModel()
        {
            VM = new List<Model_TreeViewItem>()
            {
                new Model_TreeViewItem(@"C:\ユーザー\test-MVVM")
            };
        }
    }
}
