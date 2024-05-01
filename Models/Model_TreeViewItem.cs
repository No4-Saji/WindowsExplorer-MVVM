﻿using System;
using System.Collections.Generic;
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
            this._Directory = new DirectoryInfo(path);
            if (_Directory.GetDirectories().Count() > 0)
            {
                this.Items.Add(new TreeViewItem());
                this.Expanded += Model_TreeViewItem_Expanded;
            }
            this.Header = CreateHeader();
            this.Selected += Model_TreeViewItem_Selected;
        }

        private void Model_TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (!_Expanded)
            {
                this.Items.Clear();
                foreach (DirectoryInfo dir in _Directory.GetDirectories())
                {
                    if (dir.Attributes == FileAttributes.Directory)
                    {
                        this.Items.Add(new Model_TreeViewItem(dir.FullName));
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
            _SelectionItem.Value = (this.IsSelected) ? this : (Model_TreeViewItem)e.Source;
        }
    }
}
