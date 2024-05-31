using CommunityToolkit.Mvvm.ComponentModel;

namespace WindowsExplorer.ViewModels
{
    /// <summary>
    /// MainWindow.xamlに値を渡す仲介役的なクラス
    /// </summary>
    internal class MainWindowViewModel : ObservableObject
    {
        #region プロパティ

        /// <summary>
        /// NavigationViewModelから値を得る
        /// </summary>        
        public NavigationViewModel ViewModelOfNavigation { get; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// NavigationViewModelから値を得られるようにする。
        /// </summary>
        public MainWindowViewModel()
        {
            ViewModelOfNavigation = new NavigationViewModel();
        }

        #endregion

    }
}
