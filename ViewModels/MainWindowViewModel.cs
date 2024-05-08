using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;
///　Modelのインポート
using WindowsExplorer.Models;


namespace WindowsExplorer.ViewModels
{
    internal class MainWindowViewModel : ObservableObject
    {
        
        // Modelで得られたプロパティをViewModelに引き継ぐ
        public List<Model_TreeViewItem> viewModel { get; }
        
        /// <summary>
        /// パスの引き渡し
        /// </summary>
        public MainWindowViewModel()
        {
            viewModel = new List<Model_TreeViewItem>(){new Model_TreeViewItem(@"C:\Users\test-MVVM") };
        }
    }
}
