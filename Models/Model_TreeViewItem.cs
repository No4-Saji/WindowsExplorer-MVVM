using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Reactive.Bindings;

namespace WindowsExplorer.Models
{
    public class Model_TreeViewItem : TreeViewItem
    {
        #region Fields
        // ディレクトリのパス
        public DirectoryInfo _directory { get; set; }
        // フォルダが展開されているかどうかのチェック
        private bool _expanded { get; set; } = false;
        // 選択しているフォルダを判別する
        public ReactiveProperty<Model_TreeViewItem> _selectionItem { get; set; } = new ReactiveProperty<Model_TreeViewItem>();

        #endregion

        #region Constructors

        /// <summary>
        /// コンストラクタ
        /// メソッドCreateHeader、Model_TreeViewItem_Selectedの呼び出し
        /// </summary>
        /// <param name="path">rootディレクトリ</param>
        public Model_TreeViewItem(string path)
        {
            _directory = new DirectoryInfo(path);
            if (_directory.GetDirectories().Count() > 0)
            {
                Items.Add(new TreeViewItem());
                Expanded += Model_TreeViewItem_Expanded;
            }
            
            Header = CreateHeader();
            Selected += Model_TreeViewItem_Selected;
        }
        #endregion


        #region EventHandlers
        
        /// <summary>
        /// 展開したときに子フォルダを表示する
        /// </summary>
        /// <param name="sender">イベントを発生させたオブジェクト</param>
        /// <param name="e">イベント関連の情報を持ったオブジェクト</param>
        private void Model_TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (!_expanded)
            {
                Items.Clear();
                foreach (DirectoryInfo dir in _directory.GetDirectories())
                {
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Items.Add(new Model_TreeViewItem(dir.FullName));                       
                    }
                }
                _expanded = true;
            }
        }

        /// <summary>
        /// 選択されたフォルダをプロパティに格納する
        /// </summary>
        /// <param name="sender">同上</param>
        /// <param name="e">同上</param>
        private void Model_TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            _selectionItem.Value = (IsSelected) ? this : (Model_TreeViewItem)e.Source;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Headerを生成するメソッド
        /// </summary>
        /// <returns>アイコンとフォルダ名</returns>
        private StackPanel CreateHeader()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"C:\fork\WindowsExplorer\Resources\Folder.ico", UriKind.RelativeOrAbsolute)),
                Width = 15,
                Height = 18,
            });
            sp.Children.Add(new TextBlock() { Text = _directory.Name });
            return sp;
        }
        #endregion

    }
}
