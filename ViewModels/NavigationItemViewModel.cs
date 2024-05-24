using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WindowsExplorer.Models;

namespace WindowsExplorer.ViewModels
{
    /// <summary>
    /// ツリーアイテム関連の処理
    /// </summary>
    /// <param name="directoryinfo"> ディレクトリ　</param>
    internal class NavigationItemViewModel: TreeViewItem
    {
        #region プロパティ

        /// <summary>
        /// ディレクトリ
        /// </summary>
        public DirectoryInfo Directory { get; set; }

        /// <summary>
        /// 選択しているフォルダを判別する
        /// </summary>
        public ReactiveProperty<NavigationItemViewModel> SelectionItem { get; set; } = new ReactiveProperty<NavigationItemViewModel>();

        /// <summary>
        /// FolderItemModelから値を得られるようにする。
        /// </summary>
        public List<FolderItemModel> ViewModel { get; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// NavigationItemViewModelから渡されたディレクトリをFolderItemModelへ渡す
        /// 
        /// </summary>
        /// <param name="directoryinfo"></param>
        public NavigationItemViewModel(DirectoryInfo directoryinfo)
        {
            Directory = directoryinfo;
            Header = CreateHeader();
            Selected += OnSelectedModelTreeViewItem;
            ViewModel = new List<FolderItemModel>();
            new FolderItemModel(Directory);
        }

        #endregion

        #region OnSelectedModelTreeViewItemメソッド

        /// <summary>
        /// 選択されたフォルダをプロパティに格納する
        /// </summary>
        /// <param name="sender"> イベントを発生させたオブジェクト </param>
        /// <param name="e"> イベント関連の情報を持ったオブジェクト </param>
        private void OnSelectedModelTreeViewItem(object sender, RoutedEventArgs e)
        {
            SelectionItem.Value = IsSelected ? this : (NavigationItemViewModel)e.Source;
        }

        #endregion

        #region CreateHeaderメソッド

        /// <summary>
        /// Headerを生成するメソッド
        /// </summary>
        /// <returns> アイコンとフォルダ名 </returns>
        private StackPanel CreateHeader()
        {
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"\Resources\Folder.ico", UriKind.Relative)),
                Width = 15,
                Height = 18,
            });
            sp.Children.Add(new TextBlock() { Text = Directory.Name });
            return sp;
        }

        #endregion

        #region CreateChildrenメソッド

        /// <summary>
        /// 子ディレクりを作成し、TreeViewItemに追加
        /// </summary>
        public void CreateChildren()
        {
            try
            {
                foreach (var dir in Directory.GetDirectories())
                {
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Items.Add(new FileItemModel(dir));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
