using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using WindowsExplorer.Models;


namespace WindowsExplorer.ViewModels
{

    /// <summary>
    /// Modelのコンストラクタにrootとなるパスを渡す
    /// </summary>
    internal class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Modelで得られたプロパティをViewModelに引き継ぐ
        /// </summary>        
        public List<Model_TreeViewItem> ViewModel { get; }
        public DirectoryInfo _Directory { get; set; }

        /// <summary>
        /// パスの引き渡し
        /// </summary>
        public MainWindowViewModel()
        {
            _Directory = new DirectoryInfo(@"C:\");
            ViewModel = new List<Model_TreeViewItem>(){new Model_TreeViewItem(_Directory) };
        }
    }
}
