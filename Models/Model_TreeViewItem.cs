using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
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
        ///ディレクトリ
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
        /// 渡されたディレクりのヘッダー作成、選択されているかどうかをチェック
        /// </summary>
        /// <param name="directoryInfo">ディレクトリ</param>
        /// <see cref="CreateHeader"/>
        /// <see cref="OnSelectedModelTreeViewItem"/>
        public Model_TreeViewItem(DirectoryInfo directoryInfo)
        {
            _Directory = directoryInfo;
            Header = CreateHeader();
            Selected += OnSelectedModelTreeViewItem;
        }
        #endregion


        #region イベントハンドラ

        /// <summary>
        /// 展開されていないツリービューアイテムが展開されたときの処理。
        /// 子アイテムを追加していく。
        /// </summary>
        /// <param name="e">イベント関連の情報を持ったオブジェクト</param>
        protected override void OnExpanded(RoutedEventArgs e)
        {
            Debug.WriteLine(_Expanded);
            if (_Expanded)
            {
                return;
            }

            foreach (var item in Items.OfType<Model_TreeViewItem>())
            {
                item.CreateChildren();
            }

            _Expanded = true;
        }

        /// <summary>
        /// 選択されたフォルダをプロパティに格納する
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベント関連の情報を持ったオブジェクト</param>
        private void OnSelectedModelTreeViewItem(object sender, RoutedEventArgs e)
        {
            _SelectionItem.Value = IsSelected ? this : (Model_TreeViewItem)e.Source;
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

        /// <summary>
        /// 子ディレクりを作成し、TreeViewItemに追加
        /// </summary>
        public void CreateChildren()
        {
            try
            {
                foreach (var dir in _Directory.GetDirectories())
                {
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Items.Add(new Model_TreeViewItem(dir));
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}