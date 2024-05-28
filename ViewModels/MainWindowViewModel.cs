using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using WindowsExplorer.Models;

namespace WindowsExplorer.ViewModels
{
    /// <summary>
    /// MainWindow.xamlに値を渡す仲介役的なクラス
    /// </summary>
    internal class MainWindowViewModel : ObservableObject
    {
        #region

        /// <summary>
        /// NavigationViewModelから値を得る
        /// </summary>        
        public NavigationViewModel ViewModelOfNavigation { get; }

        #endregion

        /// <summary>
        /// インスタンス化
        /// </summary>
        public MainWindowViewModel()
        {
            ViewModelOfNavigation = new NavigationViewModel();
        }
    }
}
