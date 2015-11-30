using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Model
{
    public class Watcher
    {
        public string Path { get; set; }
        public string Filter { get; set; }
        public Watcher(string path, string filter)
        {
            Path = path;
            Filter = filter;
        }

        public void Run()
        {
            var fileSystemWatcher = new FileSystemWatcher(Path, Filter)
            {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName
            };

            fileSystemWatcher.Created += (sender, args) => OnCreatedFile(new FileInfo(args.FullPath, args.ChangeType));
            fileSystemWatcher.EnableRaisingEvents = true;

            
           // while (func.Invoke()) {}
        }

        public event EventHandler<FileInfo> CreatedFile;

        protected virtual void OnCreatedFile(FileInfo e)
        {
            CreatedFile?.Invoke(this, e);
        }
    }
}
