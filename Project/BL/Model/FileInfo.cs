using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class FileInfo
    {
        public string FilePath { get; set; }
        public WatcherChangeTypes WatcherChangeType { get; set; }

        
        public FileInfo(string filePath, WatcherChangeTypes watcherChangeType)
        {
            this.FilePath = filePath;
            this.WatcherChangeType = watcherChangeType;
        }
    }
}
