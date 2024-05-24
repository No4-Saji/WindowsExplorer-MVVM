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
        public List<FileItemModel> ViewModel { get; }
        public DirectoryInfo Directory { get; set; }

        /// <summary>
        /// パスの引き渡し
        /// </summary>
        public MainWindowViewModel()
        {
            Directory = new DirectoryInfo(@"C:\");
            var vm = new FileItemModel(Directory);
            vm.CreateChildren();
            ViewModel = new List<FileItemModel>(){ vm };
        }
    }
}
