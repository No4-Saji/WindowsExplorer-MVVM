using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExplorer.Models
{
    /// <summary>
    /// フォルダー関連の処理
    /// </summary>
    /// <param name="directoryInfo">　ディレクトリ　</param>
    internal class FolderItemModel
    {
        #region プロパティ
        
        /// <summary>
        /// iconのフォルダのパス
        /// </summary>
        public static string IconUri { get; } = @"\Resources\Folder.ico";
        /// <summary>
        /// 取得したフォルダの名前
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// 取得したフォルダのパス
        /// </summary>
        public string FolderPath { get; set; }

        #endregion

        /// <summary>
        /// フォルダのパスと名前を保持する。
        /// </summary>
        /// <param name="directoryInfo"> NavigationItemViewModelから渡されたディレクトリ　</param>
        public FolderItemModel(DirectoryInfo directoryInfo)
        {
            FolderName = directoryInfo.Name;
            FolderPath = directoryInfo.FullName;
        }
    }
}
