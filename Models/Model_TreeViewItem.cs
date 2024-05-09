using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Reactive.Bindings;

namespace WindowsExplorer.Models
{
    /// <summary>
    /// フォルダをツリー構造として表示するためのクラス。
    /// ディレクトリのパスを基にサブディレクトリがある場合に展開可能なTreeViewItemを生成する。
    /// </summary>
    public class Model_TreeViewItem : TreeViewItem
    {
        #region プロパティ
        ///<summary>
        ///ディレクトリのパス
        ///</summary>
        public DirectoryInfo _Directory { get; set; }
        ///<summary>
        ///フォルダが展開されているかどうかのチェック
        ///</summary> 
        private bool _Expanded { get; set; } = false;
        /// <summary>
        /// 選択しているフォルダを判別する
        /// </summary>
        public ReactiveProperty<Model_TreeViewItem> _SelectionItem { get; set; } = new ReactiveProperty<Model_TreeViewItem>();

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 渡されたディレクりにサブディレクトリがある場合にTreeViewItemを生成
        /// </summary>
        /// <param name="path">rootディレクトリのパス</param>
        /// <see cref="Model_TreeViewItem_Expanded"/>
        /// <see cref="CreateHeader"/>
        /// <see cref="Model_TreeViewItem_Selected"/>
        public Model_TreeViewItem(string path)
        {
            try
            {
                _Directory = new DirectoryInfo(path);
                if (_Directory.GetDirectories().Count() > 0)
                {
                    Items.Add(new TreeViewItem());
                    Expanded += Model_TreeViewItem_Expanded;
                }

                Header = CreateHeader();
                Selected += Model_TreeViewItem_Selected;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("エラーが発生しました:" + ex.Message);
            }
        }
        #endregion


        #region イベントハンドラ
        
        /// <summary>
        /// 展開されていないツリービューアイテムが展開されたときの処理。
        /// 子のツリービューアイテムを初期化し、子アイテムを追加していく。
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベント関連の情報を持ったオブジェクト</param>
        private void Model_TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (!_Expanded)
            {
                Items.Clear();
                foreach (DirectoryInfo dir in _Directory.GetDirectories())
                {
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Items.Add(new Model_TreeViewItem(dir.FullName));                       
                    }
                }
                _Expanded = true;
            }
        }

        /// <summary>
        /// 選択されたフォルダをプロパティに格納する
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベント関連の情報を持ったオブジェクト</param>
        private void Model_TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            _SelectionItem.Value = (IsSelected) ? this : (Model_TreeViewItem)e.Source;
        }

        #endregion

        #region CreateHeaderメソッド
        /// <summary>
        /// Headerを生成するメソッド
        /// </summary>
        /// <returns>アイコンとフォルダ名</returns>
        private StackPanel CreateHeader()
        {
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"\Resources\Folder.ico", UriKind.Relative)),
                Width = 15,
                Height = 18,
            });
            sp.Children.Add(new TextBlock() { Text = _Directory.Name });
            return sp;
        }
        #endregion

    }
}
