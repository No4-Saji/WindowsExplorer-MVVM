using System.Diagnostics;
using System.IO;
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
        /// フォルダが展開されているかどうかのチェック
        /// </summary> 
        private bool Expanded { get; set; } = false;

        /// <summary>
        /// FolderItemModelからデータを得られるようにする
        /// </summary>
        public FolderItemModel Model { get; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// NavigationItemViewModelから渡されたディレクトリを属性として持つFolderItemModelをインスタンス化させる
        /// </summary>
        /// <param name="directoryinfo"> 渡されたディレクトリ </param>
        public NavigationItemViewModel(DirectoryInfo directoryinfo)
        {
            Header = CreateHeader(directoryinfo.Name);
            Selected += OnSelectedModelTreeViewItem;
            Model = new FolderItemModel(directoryinfo);
        }

        #endregion

        #region CreateHeaderメソッド

        /// <summary>
        /// Headerを生成するメソッド
        /// </summary>
        /// <returns> アイコンとフォルダ名 </returns>
        private StackPanel CreateHeader(string name)
        {
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(FolderItemModel.IconUri,UriKind.Relative)),
                Width = 15,
                Height = 18,
            });
            sp.Children.Add(new TextBlock() { Text = name });
            return sp;
        }

        #endregion

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
            foreach (var item in Items.OfType<NavigationItemViewModel>())
            {
                item.CreateChildren();
            }
            Expanded = true;
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
            NavigationViewModel.SelectionItem.Value = IsSelected ?  NavigationViewModel.SelectionItem.Value : (NavigationViewModel)e.Source;
        }

        #endregion


        #region CreateChildrenメソッド

        /// <summary>
        /// 子ディレクりを作成し、TreeViewItemに追加
        /// </summary>
        public void CreateChildren()
        {
            var directoryinfo = new DirectoryInfo(Model.FolderPath);
            try
            {
                foreach (var dir in directoryinfo.GetDirectories())
                {
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Items.Add(new NavigationItemViewModel(dir));
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
