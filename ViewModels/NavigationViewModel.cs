using Reactive.Bindings;
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
        /// 選択しているフォルダを判別する
        /// </summary>
        public static ReactiveProperty<NavigationViewModel> SelectionItem { get; set; } = new ReactiveProperty<NavigationViewModel>();

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

    }
}
