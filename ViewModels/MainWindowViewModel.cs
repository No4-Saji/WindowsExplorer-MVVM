using CommunityToolkit.Mvvm.ComponentModel;
using WindowsExplorer.Models;


namespace WindowsExplorer.ViewModels
{

    /// <summary>
    /// ViewModelをバインディングしている
    /// </summary>
    internal class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Modelで得られたプロパティをViewModelに引き継ぐ
        /// </summary>        
        public List<Model_TreeViewItem> ViewModel { get; }
        
        /// <summary>
        /// パスの引き渡し
        /// </summary>
        public MainWindowViewModel()
        {
            ViewModel = new List<Model_TreeViewItem>(){new Model_TreeViewItem(@"C:\") };
        }
    }
}
