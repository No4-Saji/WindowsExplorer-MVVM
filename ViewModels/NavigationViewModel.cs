using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WindowsExplorer.Models;

namespace WindowsExplorer.ViewModels
{
    /// <summary>
    /// ツリービューに関するVM
    /// </summary>
    internal class NavigationViewModel : TreeViewItem
    {
        #region プロパティ

        /// <summary>
        /// ディレクトリ
        /// </summary>
        public DirectoryInfo Directory { get; set; }

        /// <summary>
        /// フォルダが展開されているかどうかのチェック
        /// </summary> 
        private bool Expanded { get; set; } = false;

        /// <summary>
        /// NavigationItemViewModelから値をとってこれるようにする。
        /// </summary>
        public List<NavigationItemViewModel> ViewModel { get; }

        #endregion

        /// <summary>
        /// パスをNavigationItemViewModelに渡す。
        /// この段階でrootディレクトリの子ディレクトリを作成しておく。
        /// </summary>
        public NavigationViewModel() 
        {
            Directory = new DirectoryInfo(@"C:\");
            var vm = new NavigationItemViewModel(Directory);
            vm.CreateChildren();
            ViewModel = new List<NavigationItemViewModel>() { vm };
        }

        #region 展開メソッド

        /// <summary>
        /// 展開されていないツリービューアイテムが展開されたときの処理。
        /// 子アイテムを追加していく。
        /// </summary>
        /// <param name="e"> イベント関連の情報を持ったオブジェクト </param>
        protected override void OnExpanded(RoutedEventArgs e)
        {
            Debug.WriteLine(Expanded);
            if (Expanded)
            {
                return;
            }
            foreach (var item in Items.OfType<FileItemModel>())
            {
                item.CreateChildren();
            }
            Expanded = true;
        }

        #endregion

    }
}
