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
        public DirectoryInfo _Directory { get; set; }
        private bool _Expanded { get; set; } = false;
        public ReactiveProperty<Model_TreeViewItem> _SelectionItem { get; set; } = new ReactiveProperty<Model_TreeViewItem>();

        public Model_TreeViewItem(string path)
        {
            Debug.WriteLine("Test");
            Debug.WriteLine(path);
            _Directory = new DirectoryInfo(path);
            if (_Directory.GetDirectories().Count() > 0)
            {
                Items.Add(new TreeViewItem());
                Expanded += Model_TreeViewItem_Expanded;
            }
            else
            {
                Debug.WriteLine("nothing");
            }
            Header = CreateHeader();
            Selected += Model_TreeViewItem_Selected;
        }

        private void Model_TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (!_Expanded)
            {
                Items.Clear();
                foreach (DirectoryInfo dir in _Directory.GetDirectories())
                {
                    Debug.WriteLine(dir.FullName);
                    if (dir.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        Debug.WriteLine(dir.FullName);
                        Items.Add(new Model_TreeViewItem(dir.FullName));
                        
                    }
                }
                _Expanded = true;
            }
        }

        private StackPanel CreateHeader()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal };
            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"Resources\Folder.ico", UriKind.Relative)),
                Width = 15,
                Height = 18,
            });
            sp.Children.Add(new TextBlock() { Text = _Directory.Name });
            return sp;
        }

        private void Model_TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            _SelectionItem.Value = (IsSelected) ? this : (Model_TreeViewItem)e.Source;
        }
    }
}
