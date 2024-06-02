using Reactive.Bindings;
using System.IO;
using System.Windows.Controls;

namespace WindowsExplorer.ViewModels
{
    /// <summary>
    /// ツリービューに関するVM
    /// </summary>
    internal class NavigationViewModel : TreeView
    {
        #region プロパティ

        /// <summary>
        /// 選択しているフォルダを判別する
        /// </summary>
        public static ReactiveProperty<NavigationViewModel> SelectionItem { get; set; } = new ReactiveProperty<NavigationViewModel>();

        /// <summary>
        /// NavigationItemViewModelから値を得る
        /// </summary>
        public List<NavigationItemViewModel> ViewModelOfNavigationItem {get;}

        #endregion

        #region コンストラクタ

        /// <summary>
        /// パスをNavigationItemViewModelに渡す。
        /// この段階でrootディレクトリの子ディレクトリを作成しておく。
        /// </summary>
        public NavigationViewModel() 
        {
            var vm = new NavigationItemViewModel(new DirectoryInfo(@"C:\"));
            vm.CreateChildren();
            ViewModelOfNavigationItem = new List<NavigationItemViewModel>() { vm }; 
        }

        #endregion

    }
}
